import toast from "react-hot-toast";

export const printToast = (result: { error?: string; success?: string }) => {
  if (result?.error) toast.error(result.error as string);
  if (result?.success) toast.success(result.success as string);
};
