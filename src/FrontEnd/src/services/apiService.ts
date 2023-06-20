import { fetchWrapper } from './FetchWrapper.tsx';
import config from '../config.json';
import { lookup } from 'mime-types';
import { toast } from 'react-toastify';
import { authHeaderFile } from './commonConfig.ts';

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
      toast.success('File uploaded successfully', {
        position: 'top-right',
        autoClose: 3000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: 'dark',
      });
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
