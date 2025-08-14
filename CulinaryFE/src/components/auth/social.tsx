import { Icon } from "@/utils/assets/icon";
import { Button } from "../ui/button";

export default function Social() {
  return (
    <div className="flex items-center flex-col my-4">
      <Button className="w-full cursor-pointer border-mau-nau-vien" variant={"outline"} onClick={() => {}} size={"xl"}>
        <Icon.Google className="mr-1" />
        Continue with google
      </Button>
      <div className="inline-flex items-center justify-center w-full pt-10">
        <hr className="w-full h-[1px] bg-mau-nau-vien border-0 rounded-sm dark:bg-gray-700" />
        <span className="absolute px-4 -translate-x-1/2 bg-mau-be left-1/2 dark:bg-gray-900 text-sm text-gray-600">
          or with password
        </span>
      </div>
    </div>
  );
}
