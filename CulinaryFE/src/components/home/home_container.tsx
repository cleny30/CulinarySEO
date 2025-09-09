import React from "react";
import HeroSection from "./home_hero_section";
import IntroduceSection from "./home_session_2";
import { storeInfo } from "@/storeInfo";
import SectionCategoryList from "./home_category_list";

export default function HomeContainer() {
  return (
    <>
      <HeroSection />
      {storeInfo.section_introduce_show && <IntroduceSection />}
      {storeInfo.section_category_show && <SectionCategoryList />}
    </>
  );
}
