import React, { useEffect, useRef } from "react";
import {
  motion,
  MotionValue,
  useMotionValue,
  useScroll,
  useTransform,
} from "framer-motion";
import { textSkewIn } from "@/utils/constants/effect";
import { storeInfo } from "@/storeInfo";
import { Button } from "../ui/button";
import { Link } from "react-router-dom";
import { cn } from "@/lib/utils";
import styles from "@/assets/css/home.module.css";
import { DotLottieReact } from "@lottiefiles/dotlottie-react";
import { useDispatch, useSelector } from "react-redux";
import type { RootState } from "@/redux/store";
import type { Product } from "@/types/product";
import { Avatar, AvatarImage, AvatarFallback } from "../ui/avatar";
import { fetch4FeaturedProduct } from "@/redux/home/apiRequest";
import { useMediaQuery } from "@/utils/hooks/use-media-query";

function FeaturedContent({ isDesktop }: { isDesktop: boolean }) {
  return (
    <div
      className={cn(
        "flex flex-col items-center max-w-[440px] gap-y-4 z-2",
        isDesktop && styles.gridCol1,
        isDesktop && styles.gridRow1
      )}
    >
      <motion.h5
        variants={textSkewIn}
        initial={"from"}
        viewport={{ once: true }}
        whileInView={"to"}
        transition={{ duration: 0.7, delay: 0.3 }}
        className="introduce-subtext text-primary font-semibold text-base lg:text-lg md:mb-2"
      >
        {storeInfo.section_featureProduct_subtext}
      </motion.h5>
      <motion.h1
        variants={textSkewIn}
        initial={"from"}
        viewport={{ once: true }}
        whileInView={"to"}
        transition={{ duration: 0.5 }}
        className="introduce-title text-3xl lg:text-[2.5rem] font-Lucky tracking-wide leading-10 lg:leading-12 uppercase max-w-lg text-center"
      >
        {storeInfo.section_featureProduct_title}
      </motion.h1>
      <Button
        variant={"outline"}
        size={"lg"}
        className="font-Lucky tracking-wider text-base py-6 mt-2 cursor-pointer bg-transparent"
      >
        <Link to={storeInfo.section_featureProduct_btn_href}>
          {storeInfo.section_featureProduct_btn_label}
        </Link>
      </Button>
    </div>
  );
}

function FeaturedBackground({
  scrollYProgress,
}: {
  scrollYProgress: MotionValue<number>;
}) {
  const scale = useTransform(scrollYProgress, [0, 1], [0.5, 3]);
  const opacity = useTransform(scrollYProgress, [0, 1], [0, 0.7]);
  return (
    <div
      className={cn(
        styles.gridCol1,
        styles.gridRow1,
        "-z-1 w-full h-full relative"
      )}
    >
      <motion.div
        style={{ scale }}
        className="-z-2 absolute -translate-y-1/2 top-1/2"
      >
        <img src="/img/home_featuredProduct_bg.webp" alt="featuredProduct bg" />
      </motion.div>
      <motion.div
        style={{ opacity }}
        className="-z-1 absolute -translate-y-1/2 top-1/2 left-1/2 -translate-x-1/2 h-[80%] aspect-square"
      >
        <DotLottieReact
          autoplay
          loop
          src="/svg/section_featuredProduct_bgAnimated.lottie"
          className="w-full h-full"
          speed={2}
        />
      </motion.div>
    </div>
  );
}

function FeaturedProductList({
  products,
  isDesktop,
}: {
  products: Product[];
  isDesktop: boolean;
}) {
  const [hoveredIndex, setHoveredIndex] = React.useState<number | null>(null);
  return (
    <div
      className={cn(
        isDesktop && styles.gridCol1,
        isDesktop && styles.gridRow1,
        "w-full h-full relative not-md:grid not-md:grid-cols-2 not-sm:gap-5 not-md:gap-10 place-items-center"
      )}
    >
      {products[0] && (
        <ProductCard
          index={0}
          hoverIndex={hoveredIndex}
          setHoverIndex={setHoveredIndex}
          product={products[0]}
          key={products[0].productId}
          isDesktop={isDesktop}
          top={0}
          left={0}
        />
      )}
      {products[1] && (
        <ProductCard
          index={1}
          hoverIndex={hoveredIndex}
          setHoverIndex={setHoveredIndex}
          product={products[1]}
          key={products[1].productId}
          top={-30}
          right={0}
          flexDirection="flex-col-reverse"
          isDesktop={isDesktop}
        />
      )}
      {products[2] && (
        <ProductCard
          index={2}
          hoverIndex={hoveredIndex}
          setHoverIndex={setHoveredIndex}
          product={products[2]}
          key={products[2].productId}
          bottom={0}
          left={100}
          isDesktop={isDesktop}
        />
      )}
      {products[3] && (
        <ProductCard
          index={3}
          hoverIndex={hoveredIndex}
          setHoverIndex={setHoveredIndex}
          product={products[3]}
          key={products[3].productId}
          bottom={0}
          right={0}
          flexDirection="flex-row-reverse"
          isDesktop={isDesktop}
        />
      )}
    </div>
  );
}

function ProductCard({
  product,
  top,
  left,
  right,
  bottom,
  flexDirection,
  hoverIndex,
  setHoverIndex,
  index,
  isDesktop,
}: {
  product: Product;
  top?: string | number;
  left?: string | number;
  right?: string | number;
  bottom?: string | number;
  flexDirection?:
    | "flex-col"
    | "flex-row"
    | "flex-row-reverse"
    | "flex-col-reverse";
  index: number;
  hoverIndex: number | null;
  setHoverIndex: React.Dispatch<React.SetStateAction<number | null>>;
  isDesktop: boolean;
}) {
  const isDefaultFocus = index === 1;
  const isHovered = hoverIndex === index;

  const show = hoverIndex !== null ? isHovered : isDefaultFocus;

  const mouseX = useMotionValue(0);
  const mouseY = useMotionValue(0);

  const cardX = useTransform(
    mouseX,
    [-window.innerWidth / 2, window.innerWidth / 2],
    [-50, 50]
  );
  const cardY = useTransform(
    mouseY,
    [-window.innerHeight / 2, window.innerHeight / 2],
    [50, -50]
  );

  const handleMouseMove = (e: React.MouseEvent) => {
    const { clientX, clientY } = e;
    mouseX.set(clientX - window.innerWidth / 2);
    mouseY.set(clientY - window.innerHeight / 2);
  };
  return (
    <motion.div
      onMouseMove={(e) => isDesktop && handleMouseMove(e)}
      style={isDesktop ? { top, left, bottom, right } : {}}
      className={cn("md:absolute group")}
      onMouseEnter={() => isDesktop && setHoverIndex(index)}
      onMouseLeave={() => isDesktop && setHoverIndex(null)}
    >
      <motion.div style={isDesktop ? { x: cardX, y: cardY } : {}}>
        <Link
          to={`/product/${product.slug}`}
          className={cn(
            "flex gap-x-18 gap-y-4 md:gap-y-12 not-md:flex-col",
            isDesktop && flexDirection,
            flexDirection == "flex-col-reverse" ||
              flexDirection == "flex-row-reverse"
              ? "md:items-center"
              : "md:items-start"
          )}
        >
          <Avatar
            className={cn(
              show
                ? "md:opacity-100 md:scale-140"
                : "md:opacity-50 md:scale-100",
              "size-50 rounded-full bg-gray-50 duration-800"
            )}
          >
            <AvatarImage
              src={
                product.productImages[0] || "/img/section_introduce_img1.webp"
              }
            />
            <AvatarFallback>
              <AvatarImage src="/img/noproduct.webp" />
            </AvatarFallback>
          </Avatar>
          <div
            className={cn(
              show ? "md:opacity-100" : "md:opacity-0",
              "flex items-center md:items-start flex-col gap-y-1 md:gap-y-2 duration-700 relative"
            )}
          >
            <h1 className="text-mau-do-color text-2xl md:text-3xl font-Lucky">
              {new Intl.NumberFormat("vi-VN", {
                style: "currency",
                currency: "VND",
              }).format(product.price)}
            </h1>
            <h2 className="font-Lucky tracking-wide text-xl md:text-2xl">
              {product.productName}
            </h2>
            <img
              src="/svg/section_featuredProduct_arrow.svg"
              className={cn(
                flexDirection == "flex-col-reverse"
                  ? "-left-12 top-[80%] rotate-65 rotate-y-180"
                  : flexDirection == "flex-row-reverse"
                  ? "top-full rotate-y-180 -right-5"
                  : "top-[110%] -left-6",
                "absolute not-md:hidden"
              )}
            />
          </div>
        </Link>
      </motion.div>
    </motion.div>
  );
}
export default function HomeFeatureProduct() {
  const sectionRef = useRef(null);
  const dispatch = useDispatch();
  const isDesktop = useMediaQuery("(min-width: 768px)");

  // track scroll trong section
  const { scrollYProgress } = useScroll({
    target: sectionRef,
    offset: ["start start", "35% center"],
  });
  const products = useSelector(
    (state: RootState) => state.home.home.featuredProduct
  );

  useEffect(() => {
    const fetchProducts = async () => {
      await fetch4FeaturedProduct(dispatch);
    };
    if (!products) {
      fetchProducts();
    }
  }, [dispatch, products]);

  return (
    <section ref={sectionRef} className="sectionFeatureProduct md:h-[300vh] not-md:bg-mau-be">
      <div className="md:h-screen w-full overflow-hidden py-15 md:py-25 md:sticky md:top-0 not-sm:px-2">
        <div className="container mx-auto h-full">
          <div className="not-md:flex not-md:flex-col not-md:items-center not-md:gap-y-10 md:grid md:grid-rows-1 md:grid-cols-1 md:place-items-center h-full">
            <FeaturedContent isDesktop={isDesktop} />
            {isDesktop && (
              <FeaturedBackground scrollYProgress={scrollYProgress} />
            )}
            <FeaturedProductList
              products={products || []}
              isDesktop={isDesktop}
            />
          </div>
        </div>
      </div>
    </section>
  );
}
