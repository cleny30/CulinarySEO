import { storeInfo } from "@/storeInfo";
import React, { useRef, useState } from "react";
import { motion, useScroll, useTransform } from "framer-motion";
import { useMediaQuery } from "@/utils/hooks/use-media-query";
import { Button } from "../ui/button";
import { cn } from "@/lib/utils";
import { Icon } from "@/utils/assets/icon";

export default function HomeVideoSection() {
  const videoRef = useRef<HTMLVideoElement>(null);
  const [isPlaying, setIsPlaying] = useState(true);
  const isDesktop = useMediaQuery("not (max-width: 64rem)");

  // Scroll effect
  const { scrollYProgress } = useScroll({
    target: videoRef,
    offset: ["15% end", "90% end"],
    // "start end": khi top video chạm bottom viewport
    // "center center": khi center video trùng center viewport
  });

  // Scale từ 0.8 (container) -> 1 (full width)
  const scale = useTransform(scrollYProgress, [0, 1], [0.65, 1.02]);
  const y = useTransform(scrollYProgress, [0, 1], ["-15%", "0%"]);

  // Toggle play/pause
  const togglePlay = () => {
    if (!videoRef.current) return;
    if (isPlaying) {
      videoRef.current.pause();
    } else {
      videoRef.current.play();
    }
    setIsPlaying(!isPlaying);
  };
  return (
    <section className="section_video">
      <div className="overflow-hidden w-full flex items-stretch justify-center">
        <motion.div
          className="w-full overflow-hidden lg:h-200 relative flex items-stretch"
          style={{
            scale: isDesktop ? scale : "",
            y: isDesktop ? y : "",
            borderRadius: isDesktop ? "16px" : "0px",
          }}
        >
          <video
            ref={videoRef}
            className={cn("object-cover w-full")}
            poster={storeInfo.section_video_poster}
            autoPlay
            muted
            loop={true}
            playsInline
          >
            <source src={storeInfo.section_video_source} />
            <source src={storeInfo.section_video_source_alter} />
          </video>
          <div className={cn("grid place-items-center absolute inset-0 z-1")}>
            <Button
              size="icon"
              className="relative rounded-full size-20 flex items-center justify-center overflow-visible"
              onClick={() => togglePlay()}
            >
              <motion.span
                className="absolute inset-0 rounded-full bg-primary/40 z-1"
                animate={{
                  scale: [1, 1.8],
                  opacity: [0.9, 0],
                }}
                transition={{
                  duration: 1.5,
                  repeat: Infinity,
                  ease: "easeOut",
                  delay: 0.3,
                }}
              />
              <motion.span
                className="absolute inset-0 rounded-full bg-primary/40 z-2"
                animate={{
                  scale: [1, 1.5],
                  opacity: [0.8, 0],
                }}
                transition={{
                  duration: 1.2,
                  repeat: Infinity,
                  ease: "easeOut",
                }}
              />

              {isPlaying ? (
                <Icon.Pause className="size-7 relative z-10" />
              ) : (
                <Icon.Play className="size-7 relative z-10" />
              )}
            </Button>
          </div>
        </motion.div>
      </div>
    </section>
  );
}
