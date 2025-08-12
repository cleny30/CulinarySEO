import { toast as toastHot } from "react-hot-toast";

const toast = {
  success: (message: string) => toastHot.success(message),
  error: (message: string) => toastHot.error(message),
  loading: (message: string) => toastHot.loading(message),
  custom: (message: string) => toastHot.custom(message),
};

export default toast;
