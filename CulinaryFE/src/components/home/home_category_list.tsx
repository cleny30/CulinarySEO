import React from "react";
import { motion } from "framer-motion";
import { textSkewIn } from "@/utils/constants/effect";
import { storeInfo } from "@/storeInfo";
import { useSelector } from "react-redux";
import type { RootState } from "@/redux/store";
import type { Category } from "@/types/category";
import styles from "@/assets/css/home.module.css";
import { cn } from "@/lib/utils";
import { Skeleton } from "../ui/skeleton";
import { Button } from "../ui/button";
import { Link } from "react-router-dom";
import { Icon } from "@/utils/assets/icon";
import { LazyLoadImage } from "react-lazy-load-image-component";

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
function CategoryList({ categoryList }: { categoryList?: Category[] | null }) {
  const loading = useSelector((state: RootState) => state.home.home.loading);
  return (
    <div className="category_container not-xl:px-4 pt-10 pb-20 flex flex-col content-center items-stretch gap-10">
      <div className={cn(styles.categoryWrapper)}>
        {!loading
          ? categoryList?.slice(1, 5).map((item, index) => (
              <Link
                key={item.categoryName + "key"}
                to={`/collections/${item.slug}`}
                className={cn(styles.categoryItem)}
              >
                <div
                  className="group flex flex-col items-center gap-4"
                  key={index}
                >
                  <div className="relative aspect-square w-full h-auto rounded-lg  overflow-hidden">
                    <LazyLoadImage
                      className="aspect-square w-full object-cover bg-gray-50"
                      src={item.categoryImage}
                      alt={item.categoryName + "_img"}
                      placeholderSrc="/img/noproduct.webp"
                      threshold={100}
                    />
                    <h2 className="absolute z-2 left-0 bottom-4 bg-gray-50 text-sm font-Lucky p-2 pr-3 rounded-r-lg -translate-x-full group-hover:translate-x-0 duration-700">
                      Xem thêm
                    </h2>
                    <div className="absolute inset-0 z-1 bg-gradient-to-t from-mau-do-color/60 via-mau-do-color/30 to-transparent translate-y-full group-hover:translate-y-0 duration-500"></div>
                  </div>
                  <h3 className="w-full font-Lucky text-center">
                    {item.categoryName}
                  </h3>
                </div>
              </Link>
            ))
          : Array.from({ length: 4 }).map((_, index) => (
              <div className="flex flex-col items-center gap-4" key={index}>
                <Skeleton className="aspect-square w-full h-auto rounded-lg" />
                <Skeleton className="h-8 w-full rounded-lg" />
              </div>
            ))}
      </div>
      {!loading && (
        <div className="text-center">
          <Button variant={"outline"} size={"xl"} className="pr-4">
            <Link
              to={"/collections/all"}
              className="flex items-center justify-between gap-x-2"
            >
              Xem thêm danh mục khác
              <Icon.ChevronRight />
            </Link>
          </Button>
        </div>
      )}
    </div>
  );
}

export default function SectionCategoryList() {
  const categoryList = useSelector(
    (state: RootState) => state.home.home.categoryList
  );
  return (
    <section className="section_category py-4">
      <div className="container mx-auto">
        <CategoryHeader />
        <CategoryList categoryList={categoryList} />
      </div>
    </section>
  );
}
