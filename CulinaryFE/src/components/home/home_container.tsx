import React from "react";
import HeroSection from "./home_hero_section";
import IntroduceSection from "./home_session_2";
import { storeInfo } from "@/storeInfo";
import SectionCategoryList from "./home_category_list";
import SectionComboDeals from "./home_combo_deal";
import SectionOpeningHour from "./home_section_opening_hour";
import SectionHomeService from "./home_service";
import HomeVideoSection from "./home_video_section";

export default function HomeContainer() {
  return (
    <>
      <HeroSection />
      {storeInfo.section_openingHour_show && <SectionOpeningHour />}
      {storeInfo.section_introduce_show && <IntroduceSection />}
      {storeInfo.section_category_show && <SectionCategoryList />}
      {storeInfo.section_combo_show && <SectionComboDeals />}
      {storeInfo.section_video_show && <HomeVideoSection />}
      {storeInfo.section_service_show && <SectionHomeService />}
    </>
  );
}
