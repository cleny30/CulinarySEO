import { Toaster as RadToaster } from "react-hot-toast";

const Toaster = () => {
  return (
    <RadToaster
      position="top-right"
      reverseOrder={false}
      toastOptions={{
        duration: 3000,
        style: {
          fontSize: 15,
        },
        success: {
          className: "bg-green-600 text-white p-4 rounded-md",
        },
        error: {
          className: "bg-red-600 text-white p-4 rounded-md",
        },
      }}
    />
  );
};

export { Toaster };
