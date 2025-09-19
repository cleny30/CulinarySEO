import React from "react";
import { Marquee } from "../ui/marquee";
import { Icon, type IconName } from "@/utils/assets/icon";
import { storeInfo, type storeInfoKey } from "@/storeInfo";

export default function HomeMarquee() {
  return (
    <section className="section_marquee bg-primary">
      <Marquee repeat={2} >
        {Array.from({ length: 3 }).map((_, index) => {
          const IconName = storeInfo[
            `section_marquee_icon${index + 1}` as storeInfoKey
          ] as IconName;
          const SelectionIcon = Icon[IconName];
          return (
            <div key={index} className="flex items-center">
              <SelectionIcon className="size-11"/>
              <span className="p-8 font-Lucky text-xl text-white tracking-wider uppercase">
                {storeInfo[`section_marquee_text${index + 1}` as storeInfoKey]}
              </span>
            </div>
          );
        })}
      </Marquee>
    </section>
  );
}
