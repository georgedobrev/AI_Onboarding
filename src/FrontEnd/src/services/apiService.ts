import { fetchWrapper } from './FetchWrapper.tsx';
import config from '../config.json';
import { lookup } from 'mime-types';
import { toast } from 'react-toastify';
import { authHeaderFile } from './commonConfig.ts';
import { successNotification } from '../components/Notifications/Notifications.tsx';

const uploadFile = async (baseUrl: string, uploadEndpoint: string, file: File) => {
  const formData = new FormData();
  formData.append('file', file);

  const mimeType = file.type || lookup(file.name);

  let fileId;
  if (mimeType === 'application/pdf') {
    fileId = config.pdfID;
  } else if (
    mimeType === 'application/vnd.openxmlformats-officedocument.wordprocessingml.document'
  ) {
    fileId = config.docID;
  } else {
    console.error('Unsupported file type:', mimeType);
    return;
  }
  formData.append('FileTypeId', fileId);
  try {
    const response = await fetchWrapper.post(
      `${baseUrl}${uploadEndpoint}`,
      formData,
      authHeaderFile()
    );

    if (response) {
      // File uploaded successfully
      successNotification('File uploaded successfully');
      return response;
    } else {
      // Error uploading file
      console.error('Error uploading file:', response);
    }
  } catch (error) {
    console.error('Error uploading file:', error);
  }
};

export const apiService = {
  uploadFile,
};
