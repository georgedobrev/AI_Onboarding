import React, { useState } from 'react';
import AttachFileIcon from '@mui/icons-material/AttachFile';
import { Document, Page } from 'react-pdf';
import DocViewer, { DocViewerRenderers } from '@cyntler/react-doc-viewer';
import { apiService } from '../../services/apiService.ts';
import config from '../../config.json';
import './uploaded-file.css';
import { Button, CircularProgress } from '@mui/material';

const FileUploader: React.FC = () => {
  const [documentFiles, setDocumentFiles] = useState<File[]>([]);
  const [documentType, setDocumentType] = useState<string | null>(null);
  const [numPages, setNumPages] = useState<number>(0);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [loading, setLoading] = useState<boolean>(false);
  const [responseReceived, setResponseReceived] = useState<boolean>(false);

  const handleFileChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const files = (event.target as HTMLInputElement)?.files || null;

    if (files) {
      setDocumentFiles(Array.from(files));
      setCurrentPage(1);
      setLoading(true);
      setResponseReceived(false);
    }

    try {
      const baseUrl = config.baseUrl;
      const uploadEndpoint = config.uploadEndpoint;

      const responses = await Promise.all(
        Array.from(files).map((file) => apiService.uploadFile(baseUrl, uploadEndpoint, file))
      );

      if (responses.every((response) => response)) {
        const extension = files[0].name.split('.').pop()?.toLowerCase();
        if (extension === 'pdf') {
          setDocumentType('pdf');
        } else if (extension === 'docx' || extension === 'pptx') {
          setDocumentType(extension);
        } else {
          setDocumentType(null);
          console.error('Unsupported file type:', extension);
        }

        setResponseReceived(true);
      } else {
        console.error('Error uploading files:', responses);
      }
    } catch (error) {
      console.error('Error uploading files:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleDocumentLoadSuccess = ({ numPages }: { numPages: number }) => {
    setNumPages(numPages);
  };

  const handlePreviousPage = () => {
    if (responseReceived) {
      setCurrentPage((prevPage) => Math.max(prevPage - 1, 1));
    }
  };

  const handleNextPage = () => {
    if (responseReceived) {
      setCurrentPage((prevPage) => Math.min(prevPage + 1, numPages));
    }
  };

  return (
    <div className="pdf-main">
      {documentFiles.length > 0 ? (
        <div className="pdf-container">
          {loading ? (
            <div className="loading-spinner">
              <CircularProgress size={24} />
            </div>
          ) : (
            <div className="pdf-wrapper">
              {documentType === 'pdf' && (
                <Document
                  file={documentFiles[0]}
                  onLoadSuccess={handleDocumentLoadSuccess}
                  onLoadError={(error) => console.error('Error loading PDF:', error)}
                >
                  <Page
                    pageNumber={currentPage}
                    className="pdf-page"
                    renderMode="canvas"
                    renderTextLayer={false}
                    renderAnnotationLayer={false}
                  />
                </Document>
              )}
              {documentType === 'docx' && (
                <DocViewer
                  renderer={DocViewerRenderers.Docx}
                  documents={documentFiles.map((file) => URL.createObjectURL(file))}
                  onError={(error) => console.error('Error loading DOCX:', error)}
                />
              )}
              {documentType === 'pptx' && (
                <DocViewer
                  renderer={DocViewerRenderers.Pptx}
                  documents={documentFiles.map((file) => URL.createObjectURL(file))}
                  onError={(error) => console.error('Error loading PPTX:', error)}
                />
              )}
            </div>
          )}
          {responseReceived && (
            <div className="pdf-navigation">
              <Button
                onClick={handlePreviousPage}
                disabled={currentPage === 1}
                className="pdf-previous-page"
                variant="contained"
              >
                Previous Page
              </Button>
              <span>{`${currentPage} / ${numPages}`}</span>
              <Button
                onClick={handleNextPage}
                disabled={currentPage === numPages}
                className="pdf-next-page"
                variant="contained"
              >
                Next Page
              </Button>
            </div>
          )}
        </div>
      ) : null}

      <div className="attach-icon">
        <input
          type="file"
          id="file-input"
          accept=".pdf"
          style={{ display: 'none' }}
          onChange={handleFileChange}
        />
        <label htmlFor="file-input">
          <Button variant="contained" component="span" className="attach-btn">
            <AttachFileIcon />
            <p>UPLOAD FILE</p>
          </Button>
        </label>
      </div>
    </div>
  );
};

export default FileUploader;
