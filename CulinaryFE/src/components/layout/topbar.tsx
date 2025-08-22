import React from "react";
import HeaderLocation from "../header/headerLocation";
import { storeInfo } from "@/utils/config/storeInfo";

export default function TopBar() {
  return (
    <div className="flex justify-between w-full max-w-[1200px]">
      {storeInfo.topbar_content !== "" && <p>{storeInfo.topbar_content}</p>}
      <HeaderLocation />
    </div>
  );
}
