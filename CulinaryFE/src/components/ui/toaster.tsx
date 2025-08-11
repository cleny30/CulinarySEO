import { Toaster as RadToaster } from "react-hot-toast";

const Toaster = () => {
  return (
    <RadToaster
      position="top-right"
      reverseOrder={false}
      toastOptions={{
        duration: 3000,
        className: "",
        success: {
          className: "bg-green text-white",
        },
        error: {
          className: "bg-red text-white",
        },
      }}
    />
  );
};

export { Toaster };
