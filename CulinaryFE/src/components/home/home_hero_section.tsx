import { LogoMonoColor } from "@/assets/svg/logo_monocolor";
import { storeInfo } from "@/storeInfo";
import { Button } from "../ui/button";
import { Link } from "react-router-dom";
import Fade from "embla-carousel-fade";
import Autoplay from "embla-carousel-autoplay";
import { motion, useMotionValue, useTransform } from "framer-motion";
import type { MotionValue, Variants } from "framer-motion";
import {
  Carousel,
  CarouselContent,
  CarouselItem,
} from "@/components/ui/carousel";
import { Icon } from "@/utils/assets/icon";
import { textSkewIn } from "@/utils/constants/effect";

type HeroSlideImageKey =
  | "home_hero_slide_image1"
  | "home_hero_slide_image2"
  | "home_hero_slide_image3";
type HeroSlidePriceKey =
  | "home_hero_slide_price1"
  | "home_hero_slide_price2"
  | "home_hero_slide_price3";

const MotionPriceTag = ({
  variants,
  index,
}: {
  variants: Variants;
  index: number;
}) => {
  return (
    <motion.div
      className="absolute z-10 top-3 left-10 md:left-20 md:top-0 lg:top-10 lg:left-20 size-24 lg:size-32 flex flex-col items-center justify-center"
      variants={variants}
      initial="hidden"
      whileInView="visible"
    >
      <p className="text-sm lg:text-base text-white font-Lucky">Giá chỉ</p>
      <p className="text-lg lg:text-2xl text-white tracking-wider">
        <span className="font-Lucky">
          {storeInfo[`home_hero_slide_price${index + 1}` as HeroSlidePriceKey]}
        </span>
        đ
      </p>
      <Icon.SocialBg className="absolute inset-0 text-mau-do-color size-full -z-1 rotate-45" />
    </motion.div>
  );
};

function ImageBgList({
  onionX,
  onionY,
  leafX,
  leafY,
}: {
  onionX: MotionValue;
  onionY: MotionValue;
  leafX: MotionValue;
  leafY: MotionValue;
}) {
  return (
    <>
      <img
        src="/svg/hero-v1-overlay.svg"
        alt="hero overlay"
        className="absolute top-0 left-0 right-0 h-auto -z-1"
      />
      <img
        src="/svg/hero_image_edge.svg"
        alt="hero overlay"
        className="absolute bottom-0 right-0 h-auto -z-1"
      />
      <motion.div
        className="absolute left-[38%] top-[40%] z-2 not-lg:hidden"
        style={{ x: onionX, y: onionY }}
      >
        <img
          src="/img/bg_item_onion.webp"
          alt="bg_item_onion"
          className="h-18 blur-xs"
        />
      </motion.div>
      <motion.div
        className="absolute left-[35%] bottom-[10%] z-2 not-lg:hidden"
        style={{ x: leafX, y: leafY }}
      >
        <img
          src="/img/bg_item_leaf.webp"
          alt="bg_item_leaf"
          className="h-22 blur-xs"
        />
      </motion.div>
    </>
  );
}

function BannerCarousel() {
  const variants = {
    visible: {
      scale: 1,
      transition: {
        duration: 0.5,
        delay: 1,
      },
    },
    hidden: {
      scale: 0,
      transition: {
        duration: 0.5,
      },
    },
  };
  return (
    <Carousel
      plugins={[Fade({ active: true }), Autoplay({ delay: 5000 })]}
      opts={{ loop: true, align: "center" }}
      className="md:px-10 lg:px-0 z-1"
    >
      <CarouselContent>
        {Array.from({ length: 3 }).map((_, index) => (
          <CarouselItem
            key={`hero_banner_${index + 1}`}
            className="relative flex items-center"
          >
            <>
              <motion.img
                alt={`hero_banner_slide_image${index + 1}`}
                src={
                  storeInfo[
                    `home_hero_slide_image${index + 1}` as HeroSlideImageKey
                  ]
                }
                className={`h-auto w-full lg:${index + 1 == 1 && "rotate-12"}`}
                initial={{ scale: 0.1 }}
                whileInView={{ scale: 1 }}
                viewport={{ once: true }}
                transition={{ duration: 0.5 }}
              />
              <MotionPriceTag variants={variants} index={index} />
            </>
          </CarouselItem>
        ))}
      </CarouselContent>
    </Carousel>
  );
}

function LeftContent() {
  return (
    <div className="lg:basis-lg flex flex-col md:gap-y-2.5 shrink-0 pb-15">
      <motion.h5
        variants={textSkewIn}
        initial={"from"}
        animate={"to"}
        transition={{ duration: 1, delay: 0.3 }}
        className="sub-text text-primary font-semibold text-lg mb-2"
      >
        {storeInfo.home_hero_subtext}
      </motion.h5>
      <motion.h1
        variants={textSkewIn}
        initial={"from"}
        animate={"to"}
        transition={{ duration: 0.7 }}
        className="hero-title text-3xl md:text-4xl lg:text-5xl font-Lucky tracking-wide leading-10 lg:leading-15"
      >
        {storeInfo.home_hero_title}
      </motion.h1>
      <motion.p
        variants={textSkewIn}
        initial={"from"}
        animate={"to"}
        transition={{ duration: 0.7 }}
        className="hero-subtitle text-base pr-10"
      >
        {storeInfo.home_hero_subtitle}
      </motion.p>
      <motion.div
        variants={textSkewIn}
        initial={"from"}
        animate={"to"}
        className="hero-cta mt-4"
        transition={{ duration: 0.7, delay: 0.3 }}
      >
        <Button
          variant={"outline"}
          className="border-2 font-Lucky text-primary p-5 md:p-6 tracking-wide"
        >
          <Link to={storeInfo.home_hero_cta_href}>
            {storeInfo.home_hero_cta_label}
          </Link>
        </Button>
      </motion.div>
    </div>
  );
}
export default function HeroSection() {
  const mouseX = useMotionValue(0);
  const mouseY = useMotionValue(0);

  const onionX = useTransform(
    mouseX,
    [-window.innerWidth / 2, window.innerWidth / 2],
    [10, -10]
  );
  const onionY = useTransform(
    mouseY,
    [-window.innerHeight / 2, window.innerHeight / 2],
    [-10, 10]
  );
  const leafX = useTransform(
    mouseX,
    [-window.innerWidth / 2, window.innerWidth / 2],
    [50, -50]
  );
  const leafY = useTransform(
    mouseY,
    [-window.innerHeight / 2, window.innerHeight / 2],
    [-30, 30]
  );

  const handleMouseMove = (e: React.MouseEvent) => {
    const { clientX, clientY } = e;
    mouseX.set(clientX - window.innerWidth / 2);
    mouseY.set(clientY - window.innerHeight / 2);
  };

  return (
    <motion.section
      className="section_home_hero relative overflow-hidden pt-35 pb-10 z-1 px-4"
      onMouseMove={(e) => handleMouseMove(e)}
    >
      <div className="container mx-auto pt-5 lg:h-[75vh] z-1">
        <LogoMonoColor
          className="mx-auto w-[95%] not-lg:hidden -z-1 relative -mb-8"
          initial={{ y: "50%", opacity: 0 }}
          animate={{ y: "0%", opacity: 1 }}
          transition={{
            y: { duration: 1, ease: "easeInOut" },
            opacity: { duration: 1, ease: "linear" },
          }}
        />
        <div className="flex items-center gap-2 not-lg:h-full not-lg:flex-col">
          <LeftContent />
          <BannerCarousel />
        </div>
      </div>
      <ImageBgList
        onionX={onionX}
        onionY={onionY}
        leafX={leafX}
        leafY={leafY}
      />
    </motion.section>
  );
}
