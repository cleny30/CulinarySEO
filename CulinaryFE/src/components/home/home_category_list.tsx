import React from "react";
import { motion } from "framer-motion";
import { textSkewIn } from "@/utils/constants/effect";
import { storeInfo } from "@/storeInfo";
import {
  Carousel,
  CarouselContent,
  CarouselItem,
  CarouselNext,
  CarouselPrevious,
} from "@/components/ui/carousel";
import { useSelector } from "react-redux";
import type { RootState } from "@/redux/store";
import type { CategoryCount } from "@/types/category";

function CategoryHeader() {
  return (
    <div className="category_header flex flex-col items-center">
      <motion.h5
        variants={textSkewIn}
        initial={"from"}
        viewport={{ once: true }}
        whileInView={"to"}
        transition={{ duration: 0.7, delay: 0.3 }}
        className="introduce-subtext text-primary font-semibold text-base lg:text-lg mb-2"
      >
        {storeInfo.section_category_subtext}
      </motion.h5>
      <motion.h1
        variants={textSkewIn}
        initial={"from"}
        viewport={{ once: true }}
        whileInView={"to"}
        transition={{ duration: 0.5 }}
        className="introduce-title text-3xl lg:text-[2.5rem] font-Lucky tracking-wide leading-10 lg:leading-12 uppercase max-w-lg text-center"
      >
        {storeInfo.section_category_title}
      </motion.h1>
    </div>
  );
}
function CategoryList({categoryList}:{categoryList?:CategoryCount}) {
  return (
    <div>
      <Carousel>
        <CarouselContent></CarouselContent>
      </Carousel>
    </div>
  );
}

export default function SectionCategoryList() {
  const categoryList = useSelector(
    (state: RootState) => state.home.header.categoryItem
  );
  return (
    <section className="section_category">
      <div className="container mx-auto">
        <CategoryHeader />
        <CategoryList />
      </div>
    </section>
  );
}
