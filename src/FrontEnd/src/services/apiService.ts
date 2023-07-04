import { fetchWrapper } from './FetchWrapper.tsx';
import config from '../config.json';
import { lookup } from 'mime-types';
import { authHeaderFile } from './commonConfig.ts';
import { successNotification } from '../components/Notifications/Notifications.tsx';
import { authService } from './authService.ts';

const uploadFile = async (baseUrl: string, uploadEndpoint: string, formData: FormData) => {
  try {
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

const convertFileToPdf = async (file: File) => {
  const formData = new FormData();
  formData.append('file', file);

  const mimeType = file.type || lookup(file.name);

  let fileId;
  if (mimeType === 'application/pdf') {
    fileId = config.pdfID;
    formData.append('FileTypeId', fileId);
    return uploadFile(config.baseUrl, config.convertFileEndpoint, formData);
  } else if (
      mimeType === 'application/vnd.openxmlformats-officedocument.wordprocessingml.document'
  ) {
    fileId = config.docID;
    formData.append('FileTypeId', fileId);
    const convertedResponse = await authService.convertFile(formData);

    if (convertedResponse) {
      const convertedPdfByteArray = new Uint8Array(convertedResponse);
      const convertedPdfBlob = new Blob([convertedPdfByteArray], { type: 'application/pdf' });
      const convertedFile = new File([convertedPdfBlob], file.name, { type: 'application/pdf' });

      return uploadFile(config.baseUrl, config.uploadEndpoint, convertedResponse);
    } else {
      console.error('Error converting file:', convertedResponse);
    }
  } else {
    console.error('Unsupported file type:', mimeType);
    return null;
  }
};

export const apiService = {
  uploadFile,
  convertFileToPdf,
};
