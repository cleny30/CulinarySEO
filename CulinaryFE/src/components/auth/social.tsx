import {
  GoogleLogin,
  type CredentialResponse,
} from "@react-oauth/google";
import { loginGoogle } from "@/redux/auth/apiRequest";
import { useDependencyInjection } from "@/utils/hooks/useDependencyInjection";
import { printToast } from "@/utils/constants/toast/printToast";

export default function Social() {
  const { dispatch, navigate } = useDependencyInjection();

  const handleSuccess = async (credentialResponse: CredentialResponse) => {
    if (credentialResponse.credential) {
      await loginGoogle(credentialResponse.credential, dispatch, navigate).then(
        printToast
      );
    }
  };

  // useGoogleOneTapLogin({
  //   onSuccess: handleSuccess,
  // });

  return (
    <div className="flex items-center flex-col my-4">
      {/* <Button
        className="w-full cursor-pointer border-mau-nau-vien"
        variant={"outline"}
        onClick={() => handleLogin()}
        size={"xl"}
      >
        <Icon.Google className="mr-1" />
        Continue with google
      </Button> */}
      <GoogleLogin
        onSuccess={handleSuccess}
        theme="outline"
        shape="rectangular"
        width={100}
        type="standard"
        useOneTap
      />
      <div className="inline-flex items-center justify-center w-full pt-10">
        <hr className="w-full h-[1px] bg-mau-nau-vien border-0 rounded-sm dark:bg-gray-700" />
        <span className="absolute px-4 -translate-x-1/2 bg-white left-1/2 dark:bg-gray-900 text-sm text-gray-600">
          or with password
        </span>
      </div>
    </div>
  );
}
