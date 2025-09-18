import SectionCategoryList from "@/components/home/home_category_list";
import SectionComboDeals from "@/components/home/home_combo_deal";
import HomeFeatureProduct from "@/components/home/home_feature_product";
import HeroSection from "@/components/home/home_hero_section";
import SectionOpeningHour from "@/components/home/home_section_opening_hour";
import SectionHomeService from "@/components/home/home_service";
import SectionIntroduce from "@/components/home/home_session_2";
import HomeVideoSection from "@/components/home/home_video_section";
import { storeInfo } from "@/storeInfo";
export default function HomePage() {
  return (
    <>
      <HeroSection />
      {storeInfo.section_openingHour_show && <SectionOpeningHour />}
      {storeInfo.section_introduce_show && <SectionIntroduce />}
      {storeInfo.section_category_show && <SectionCategoryList />}
      {storeInfo.section_combo_show && <SectionComboDeals />}
      {storeInfo.section_video_show && <HomeVideoSection />}
      {storeInfo.section_service_show && <SectionHomeService />}
      {storeInfo.section_featureProduct_show && <HomeFeatureProduct />}
    </>
  );
}
