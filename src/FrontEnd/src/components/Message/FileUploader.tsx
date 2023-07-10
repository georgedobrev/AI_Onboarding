import React, { useState, useEffect } from 'react';
import { Document, Page } from 'react-pdf';
import { AttachFile, Close } from '@mui/icons-material';
import { Button, CircularProgress } from '@mui/material';
import { KeyboardArrowLeft, KeyboardArrowRight } from '@mui/icons-material';
import { apiService } from '../../services/apiService.ts';
import config from '../../config.json';
import './FileUploader.css';

const FileUploader: React.FC = () => {
  const [documentFiles, setDocumentFiles] = useState<File | null | undefined>(null);
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
                <KeyboardArrowLeft />
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
                <KeyboardArrowRight />
              </Button>
            </div>
          )}
          {!loading && responseReceived && (
            <div className="pdf-navigation">
              <span>{`${currentPage} / ${numPages}`}</span>
            </div>
          )}
          {!loading && !responseReceived && (
            <div className="pdf-navigation">
              <span>Uploading...</span>
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
              <AttachFile />
              <p>ATTACH FILE</p>
            </Button>
          </label>
        </div>
      ) : (
        <div className="upload-icon">
          <Button variant="contained" className="upload-btn" onClick={handleUploadFile}>
            <AttachFile />
            <p>UPLOAD FILE</p>
          </Button>
          <Button onClick={handleResetFile} variant="outlined" className="reset-file-button">
            <Close />
          </Button>
        </div>
      )}
    </div>
  );
};

export default FileUploader;
