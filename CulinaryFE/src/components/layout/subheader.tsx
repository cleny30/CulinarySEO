import React from "react";
import HeaderSearch from "../header/headerSearch";
import HeaderLocation from "../header/headerLocation";

export default function SubHeader() {
  return (
    <div className="flex justify-end w-full max-w-[1200px]">
      <HeaderSearch />
      <HeaderLocation />
    </div>
  );
}
