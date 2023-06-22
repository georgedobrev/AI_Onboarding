import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

export const successNotification = (message, options = {}) => {
  toast.success(message, {
    position: options.position || 'top-right',
    autoClose: options.autoClose || 1000,
    hideProgressBar: options.hideProgressBar || true,
    closeOnClick: options.closeOnClick || true,
    pauseOnHover: options.pauseOnHover || true,
    draggable: options.draggable || true,
    progress: options.progress || undefined,
    theme: options.theme || 'dark',
  });
};

export const errorNotifications = (message, options = {}) => {
  toast.error(message, {
    position: options.position || 'top-right',
    autoClose: options.autoClose || 1000,
    hideProgressBar: options.hideProgressBar || true,
    closeOnClick: options.closeOnClick || true,
    pauseOnHover: options.pauseOnHover || true,
    draggable: options.draggable || true,
    progress: options.progress || undefined,
    theme: options.theme || 'dark',
  });
};
