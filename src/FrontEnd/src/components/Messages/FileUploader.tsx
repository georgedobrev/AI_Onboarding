import React, { useState } from 'react';
import AttachFileIcon from '@mui/icons-material/AttachFile';
import IconButton from '@mui/material/IconButton';
import { Document, Page } from 'react-pdf';

const FileUploader: React.FC = () => {
  const [pdfFile, setPdfFile] = useState<File | null>(null);
  const [numPages, setNumPages] = useState<number>(0);
  const [currentPage, setCurrentPage] = useState<number>(1);

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0] || null;

    if (file) {
      setPdfFile(file);
      setCurrentPage(1);
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
      <div>
        {pdfFile ? (
            <div>
              <div>
                <Document
                    file={pdfFile}
                    onLoadSuccess={handleDocumentLoadSuccess}
                    onLoadError={(error) => console.error('Error loading PDF:', error)}
                >
                  <Page pageNumber={currentPage} />
                </Document>
              </div>
              <div>
                <IconButton onClick={handlePreviousPage} disabled={currentPage === 1}>
                  Previous Page
                </IconButton>
                <span>{`${currentPage} / ${numPages}`}</span>
                <IconButton onClick={handleNextPage} disabled={currentPage === numPages}>
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
