import React, { useState } from 'react';
import AttachFileIcon from '@mui/icons-material/AttachFile';
import { Document, Page } from 'react-pdf';
import { apiService } from '../../services/apiService.ts';
import config from '../../config.json';
import './uploaded-file.css';
import { Button, CircularProgress } from '@mui/material';

const FileUploader: React.FC = () => {
  const [pdfFile, setPdfFile] = useState<File | null>(null);
  const [numPages, setNumPages] = useState<number>(0);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [loading, setLoading] = useState<boolean>(false); // Added loading state

  const handleFileChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = (event.target as HTMLInputElement)?.files?.[0] || null;

    if (file) {
      setPdfFile(file);
      setCurrentPage(1);
      setLoading(true); // Show loading spinner when file is selected
    }

    try {
      const baseUrl = config.baseUrl;
      const uploadEndpoint = config.uploadEndpoint;
      const response = await apiService.uploadFile(baseUrl, uploadEndpoint, file);
      if (response) {
        // File uploaded successfully
      } else {
        // Error uploading file
        console.error('Error uploading file:', response);
      }
    } catch (error) {
      console.error('Error uploading file:', error);
    } finally {
      setLoading(false); // Hide loading spinner after API response
    }
  };

  const handleDocumentLoadSuccess = ({ numPages }: { numPages: number }) => {
    setNumPages(numPages);
  };

  const handlePreviousPage = () => {
    setCurrentPage((prevPage) => Math.max(prevPage - 1, 1));
  };

  const handleNextPage = () => {
    setCurrentPage((prevPage) => Math.min(prevPage + 1, numPages));
  };

  return (
    <div className="pdf-main">
      {pdfFile ? (
        <div className="pdf-container">
          {loading ? ( // Display loading spinner while waiting for API response
            <div className="loading-spinner">
              <CircularProgress size={24} />
            </div>
          ) : (
            <div className="pdf-wrapper">
              <Document
                file={pdfFile}
                onLoadSuccess={handleDocumentLoadSuccess}
                onLoadError={(error) => console.error('Error loading PDF:', error)}
              >
                <Page
                  pageNumber={currentPage}
                  className="pdf-page"
                  renderMode="canvas"
                  renderTextLayer={false}
                  renderAnnotationLayer={false}
                  renderInteractiveForms={false}
                />
              </Document>
            </div>
          )}
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
