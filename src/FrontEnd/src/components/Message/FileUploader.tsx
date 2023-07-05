import React, { useState, useEffect } from 'react';
import { apiService } from '../../services/apiService.ts';
import { Document, Page } from 'react-pdf';
import AttachFileIcon from '@mui/icons-material/AttachFile';
import CloseIcon from '@mui/icons-material/Close';
import { Button, CircularProgress } from '@mui/material';
import KeyboardArrowLeftIcon from '@mui/icons-material/KeyboardArrowLeft';
import KeyboardArrowRightIcon from '@mui/icons-material/KeyboardArrowRight';
import config from '../../config.json';
import './FileUploader.css';

const FileUploader: React.FC = () => {
  const [documentFiles, setDocumentFiles] = useState<File | null>(null);
  const [documentType, setDocumentType] = useState<string | null>(null);
  const [numPages, setNumPages] = useState<number>(0);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [loading, setLoading] = useState<boolean>(false);
  const [responseReceived, setResponseReceived] = useState<boolean>(false);
  const acceptedFileTypes = '.pdf, .docx, .pptx';

  useEffect(() => {
    if (documentFiles) {
      setCurrentPage(1);
      setLoading(true);
      setResponseReceived(false);
      handleFileUpload();
    }
  }, [documentFiles]);

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const files = (event.target as HTMLInputElement)?.files;
    if (files && files.length > 0) {
      setDocumentFiles(files[0]);
    }
  };

  const handleFileUpload = async () => {
    try {
      setLoading(true);
      const responses = await apiService.displayFile(documentFiles);
      setDocumentFiles(responses);
      setDocumentType('pdf');
      setResponseReceived(true);
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

  const handleResetFile = () => {
    setDocumentFiles(null);
    setResponseReceived(false);
  };

  const handleUploadFile = async () => {
    if (documentFiles) {
      setLoading(true);
      try {
        const baseUrl = config.baseUrl;
        const uploadUrl = config.uploadEndpoint;
        await apiService.uploadFile(baseUrl, uploadUrl, documentFiles);
      } catch (error) {
        console.error('Error uploading file:', error);
      }
      setLoading(false);
    }
  };

  return (
    <div className="pdf-main">
      {documentFiles ? (
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
              {documentType === 'pdf' && (
                <Document
                  file={documentFiles}
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
      {!documentFiles ? (
        <div className="attach-icon">
          <input
            type="file"
            id="file-input"
            accept={acceptedFileTypes}
            style={{ display: 'none' }}
            onChange={handleFileChange}
          />
          <label htmlFor="file-input">
            <Button variant="contained" component="span" className="attach-btn">
              <AttachFileIcon />
              <p>ATTACH FILE</p>
            </Button>
          </label>
        </div>
      ) : (
        <div className="upload-icon">
          <Button variant="contained" className="upload-btn" onClick={handleUploadFile}>
            <AttachFileIcon />
            <p>UPLOAD FILE</p>
          </Button>
          <Button onClick={handleResetFile} variant="outlined" className="reset-file-button">
            <CloseIcon />
          </Button>
        </div>
      )}
    </div>
  );
};

export default FileUploader;
