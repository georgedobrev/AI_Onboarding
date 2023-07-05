import { fetchWrapper } from './FetchWrapper.tsx';
import config from '../config.json';
import { lookup } from 'mime-types';
import { authService } from './authService.ts';
import { authHeaderFile } from './commonConfig.ts';
import { successNotification } from '../components/Notifications/Notifications.tsx';
const uploadFile = async (baseUrl: string, uploadEndpoint: string, file: File) => {
  const formData = new FormData();
  const mimeType = file.type || lookup(file.name);
  formData.append('file', file);
  let fileId: string;

  try {
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
    const response = await fetchWrapper.post(
      `${baseUrl}${uploadEndpoint}`,
      formData,
      authHeaderFile()
    );

    if (response) {
      successNotification('File uploaded successfully');
      return response;
    } else {
      console.error('Error uploading file:', response);
    }
  } catch (error) {
    console.error('Error uploading file:', error);
  }
};

const displayFile = async (file: File) => {
  const mimeType = file.type || lookup(file.name);
  const formData = new FormData();
  let fileId;

  function base64ToArrayBuffer(base64: string) {
    const binaryString = window.atob(base64);
    return new Uint8Array(Array.from(binaryString, (char) => char.charCodeAt(0)));
  }

  if (mimeType === 'application/pdf') {
    return file;
  } else if (
    mimeType === 'application/vnd.openxmlformats-officedocument.wordprocessingml.document'
  ) {
    fileId = config.docID;
    formData.append('file', file);
    formData.append('FileTypeId', fileId);
    const response = await authService.convertFile(formData);

    return new File([base64ToArrayBuffer(String(response))], 'file name', {
      type: 'application/pdf',
    });
  } else {
    console.error('Unsupported file type:', mimeType);
    return;
  }
};

export const apiService = {
  uploadFile,
  displayFile,
};
