import { LogoMonoColor } from "@/assets/svg/logo_monocolor";
import { storeInfo } from "@/storeInfo";
import { Button } from "../ui/button";
import { Link } from "react-router-dom";
import Fade from "embla-carousel-fade";
import Autoplay from "embla-carousel-autoplay";
import { motion, useAnimation } from "framer-motion";
import type { Variants } from "framer-motion";
import {
  Carousel,
  CarouselContent,
  CarouselItem,
} from "@/components/ui/carousel";
import { Icon } from "@/utils/assets/icon";

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
      className="absolute z-10 top-4 left-8 size-32 flex flex-col items-center justify-center"
      variants={variants}
      initial="hidden"
      whileInView="visible"
    >
      <p className="text-base text-white font-Lucky">Giá chỉ</p>
      <p className="text-2xl text-white tracking-wider">
        <span className="font-Lucky">
          {storeInfo[`home_hero_slide_price${index + 1}` as HeroSlidePriceKey]}
        </span>
        đ
      </p>
      <Icon.SocialBg className="absolute inset-0 text-mau-do-color size-full -z-1 rotate-45" />
    </motion.div>
  );
};

export default function HeroSection() {
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
  const imgAnimation = useAnimation();
  const handleMouseMove = (e: React.MouseEvent) => {
    const { clientX, clientY } = e;
    const moveX = clientX - window.innerWidth / 2;
    const moveY = clientY - window.innerHeight / 2;
    const offsetFactor = 50;
    imgAnimation.start({
      x: moveX / offsetFactor,
      y: moveY / offsetFactor,
    });
  };

  return (
    <motion.section className="section_home_hero relative overflow-hidden pt-40 z-1 px-4" onMouseMove={(e) => handleMouseMove(e)}>
      <div className="container mx-auto pt-5 h-[75vh] z-1">
        <LogoMonoColor className="text-primary/10 h-50 mx-auto z-1 relative" />
        <div className="flex items-center gap-x-2">
          <div className="basis-lg flex flex-col gap-y-2.5 shrink-0">
            <h5 className="sub-text text-primary font-semibold text-lg mb-2">
              {storeInfo.home_hero_subtext}
            </h5>
            <h1 className="hero-title text-5xl font-Lucky tracking-wide leading-15">
              {storeInfo.home_hero_title}
            </h1>
            <p className="hero-subtitle text-base pr-10">
              {storeInfo.home_hero_subtitle}
            </p>
            <div className="hero-cta mt-4">
              <Button
                variant={"outline"}
                size={"xl"}
                className="border-2 font-Lucky text-primary py-6 tracking-wide"
              >
                <Link to={storeInfo.home_hero_cta_href}>
                  {storeInfo.home_hero_cta_label}
                </Link>
              </Button>
            </div>
          </div>
          <div>
            <Carousel
              plugins={[Fade({ active: true }), Autoplay({ delay: 5000 })]}
              opts={{ loop: true, align: "center" }}
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
                            `home_hero_slide_image${
                              index + 1
                            }` as HeroSlideImageKey
                          ]
                        }
                        className="h-auto w-full"
                        initial={{ scale: 0.1 }}
                        whileInView={{ scale: 1 }}
                        transition={{ duration: 0.5 }}
                      />
                      <MotionPriceTag variants={variants} index={index} />
                    </>
                  </CarouselItem>
                ))}
              </CarouselContent>
            </Carousel>
          </div>
        </div>
      </div>
      <img
        src="/svg/hero-v1-overlay.svg"
        alt="hero overlay"
        className="absolute top-0 left-0 right-0 h-auto -z-1"
      />
      <img
        src="/svg/hero_image_edge.svg"
        alt="hero overlay"
        className="absolute bottom-0 right-0 h-auto"
      />
      <motion.div
        className="absolute left-[40%] top-[40%] z-2"
        animate={imgAnimation}
      >
        <img
          src="/img/bg_item_onion.webp"
          alt="bg_item_tomato_footer"
          className="size-20 blur-xs"
        />
      </motion.div>
      <motion.div
        className="absolute left-[35%] bottom-[10%] z-2"
        animate={imgAnimation}
      >
        <img
          src="/img/bg_item_leaf.webp"
          alt="bg_item_tomato_footer"
          className="h-20 blur-xs"
        />
      </motion.div>
    </motion.section>
  );
}
