import React from "react";
import HeroSection from "./home_hero_section";
import IntroduceSection from "./home_session_2";
import { storeInfo } from "@/storeInfo";

export default function HomeContainer() {
  return (
    <>
      <HeroSection />
      {storeInfo.section_introduce_show && <IntroduceSection />}
    </>
  );
}
