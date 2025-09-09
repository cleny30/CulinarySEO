import React from "react";
import HeaderLocation from "../header/headerLocation";
import { storeInfo } from "@/storeInfo";

export default function TopBar() {
  return (
    <div className="flex justify-between items-center w-full container">
      <div></div>
      {storeInfo.topbar_content !== "" && (
        <p className="text-base text-white font-normal">
          {storeInfo.topbar_content}
        </p>
      )}
      <HeaderLocation />
    </div>
  );
}
