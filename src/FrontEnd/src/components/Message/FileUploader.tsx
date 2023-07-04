import React, { useState } from 'react';
import { apiService } from '../../services/apiService.ts';
import config from '../../config.json';
import { Document, Page } from 'react-pdf';
import AttachFileIcon from '@mui/icons-material/AttachFile';
import { Button, CircularProgress } from '@mui/material';
import KeyboardArrowLeftIcon from '@mui/icons-material/KeyboardArrowLeft';
import KeyboardArrowRightIcon from '@mui/icons-material/KeyboardArrowRight';
import './FileUploader.css';

const FileUploader: React.FC = () => {
  const [documentFiles, setDocumentFiles] = useState<File[]>([]);
  const [documentType, setDocumentType] = useState<string | null>(null);
  const [numPages, setNumPages] = useState<number>(0);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [loading, setLoading] = useState<boolean>(false);
  const [responseReceived, setResponseReceived] = useState<boolean>(false);
  const [pdfByteArray, setPdfByteArray] = useState<Uint8Array | null>(null);

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
          Array.from(files).map((file) => apiService.convertFileToPdf(file))
      );
      console.log(responses);

      if (responses.every((response) => response)) {
        setResponseReceived(true);
        setDocumentType('pdf');

        const pdfResponse = responses[0];

        setPdfByteArray(pdfResponse);
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
              <Button
                onClick={handlePreviousPage}
                disabled={currentPage === 1}
                className="pdf-previous-page"
                variant="contained"
              >
                <KeyboardArrowLeftIcon />
              </Button>
              {documentType === 'pdf' && pdfByteArray && (
                  <Document
                      file={{ data: pdfByteArray  }}
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
              <Button
                onClick={handleNextPage}
                disabled={currentPage === numPages}
                className="pdf-next-page"
                variant="contained"
              >
                <KeyboardArrowRightIcon />
              </Button>
            </div>
          )}
          {responseReceived && (
            <div className="pdf-navigation">
              <span>{`${currentPage} / ${numPages}`}</span>
            </div>
          )}
        </div>
      ) : null}

      <div className="attach-icon">
        <input
          type="file"
          id="file-input"
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
