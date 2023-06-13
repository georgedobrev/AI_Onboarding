import React, { ChangeEvent } from 'react';
import AttachFileIcon from '@mui/icons-material/AttachFile';
import IconButton from '@mui/material/IconButton';
import { apiService } from '../../services/apiService.ts';
import config from '../../config.json';

const FileUploader: React.FC = () => {
  const handleFileChange = async (event: ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0] || null;

    if (!file) return;

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

  const handleButtonClick = () => {
    const fileInput = document.createElement('input');
    fileInput.type = 'file';
    fileInput.accept = '.pdf,.docx';
    fileInput.addEventListener('change', handleFileChange);
    fileInput.click();
  };

  return (
    <IconButton onClick={handleButtonClick} className="attach-icon">
      <AttachFileIcon className="attach-btn" />
    </IconButton>
  );
};
export default FileUploader;
