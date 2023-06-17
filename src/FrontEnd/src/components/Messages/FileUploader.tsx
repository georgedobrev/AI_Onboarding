import React, { useState } from 'react';
import AttachFileIcon from '@mui/icons-material/AttachFile';
import IconButton from '@mui/material/IconButton';
import { Document, Page } from 'react-pdf';
import {apiService} from "../../services/apiService.ts";
import config from "../../config.json"
import './uploaded-file.css'

const FileUploader: React.FC = () => {
  const [pdfFile, setPdfFile] = useState<File | null>(null);
  const [numPages, setNumPages] = useState<number>(0);
  const [currentPage, setCurrentPage] = useState<number>(1);

  const handleFileChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = (event.target as HTMLInputElement)?.files?.[0] || null;

    if (file) {
      setPdfFile(file);
      setCurrentPage(1);
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
          <div className="pdf-navigation">
            <IconButton
              onClick={handlePreviousPage}
              disabled={currentPage === 1}
              className="pdf-previous-page"
            >
              Previous Page
            </IconButton>
            <span>{`${currentPage} / ${numPages}`}</span>
            <IconButton
              onClick={handleNextPage}
              disabled={currentPage === numPages}
              className="pdf-next-page"
            >
              Next Page
            </IconButton>
          </div>
        </div>
      ) : (
        <IconButton component="label" htmlFor="file-input" className="attach-icon">
          <input
            type="file"
            id="file-input"
            accept=".pdf"
            style={{ display: 'none' }}
            onChange={handleFileChange}
          />
          <AttachFileIcon className="attach-btn" />
        </IconButton>
      )}
    </div>
  );
};

export default FileUploader;
