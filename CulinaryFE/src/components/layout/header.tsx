import { useDispatch, useSelector } from "react-redux";
import type { RootState } from "@/redux/store";
import HeaderLogo from "../header/header_logo";
import HeaderNav from "../header/header_nav";
import HeaderRightActions from "../header/header_right_actions";
import SubHeader from "./subheader";
import { cn } from "@/lib/utils";
import TopBar from "./topbar";
import { useLayoutEffect, useRef } from "react";
import { fetchCateMenu } from "@/redux/home/apiRequest";
import HeaderNavMobile from "../header/header_nav-mobile";
import { useScroll, useTransform, motion, useAnimation } from "framer-motion";

interface HeaderProps {
  headerStyle?: "1" | "2" | "3";
  subHeader?: boolean;
  topbar?: boolean;
}

const MotionNav = motion.nav;
const MotionHeader = motion.header;

export default function Header({
  subHeader = true,
  headerStyle = "1",
  topbar = true,
}: HeaderProps) {
  const dispatch = useDispatch();
  const user =
    useSelector((state: RootState) => state.auth.login?.currentUser) || null;
  const headerContainerStyle = "w-full flex justify-center";

  useLayoutEffect(() => {
    const getCate = async () => {
      await fetchCateMenu(dispatch);
    };
    getCate();
  }, [dispatch]);

  const { scrollY } = useScroll();
  const scrollYRange = [0, 100, 100];
  const useMotionValueScrollYFactory = (values: unknown[]) => {
    return useTransform(scrollY, scrollYRange, values);
  };

  const headerBackground = useMotionValueScrollYFactory([
    "rgba(255, 250, 238,0)",
    "rgba(255, 250, 238,1)",
    "rgba(255, 250, 238,1)",
  ]);
  const imageHeight = useMotionValueScrollYFactory([50, 40, 40]);
  const topBarShow = useMotionValueScrollYFactory(["40px", "0px", "0px"]);
  const topBarOpacity = useMotionValueScrollYFactory([1, 0, 0]);
  const topBarPadding = useMotionValueScrollYFactory([
    "0.25rem",
    "0.25rem",
    "0rem",
  ]);
  const navHeight = useMotionValueScrollYFactory(["5rem", "5rem", "3.5rem"]);
  const headerVariants = {
    visible: { top: "0%" },
    hidden: { top: "-100px" },
  };

  const controls = useAnimation();
  const delta = useRef(0);
  const lastScrollY = useRef(0);
  scrollY.onChange((val) => {
    const diff = Math.abs(val - lastScrollY.current);
    if (val >= lastScrollY.current) {
      delta.current = delta.current >= 10 ? 10 : delta.current + diff;
    } else {
      delta.current = delta.current <= -10 ? -10 : delta.current - diff;
    }

    if (delta.current >= 10 && val > 400) {
      controls.start("hidden");
    } else if (delta.current <= -10 || val < 200) {
      controls.start("visible");
    }
    lastScrollY.current = val;
  });

  return (
    <>
      {headerStyle === "1" && (
        <MotionHeader
          className="w-full flex flex-col fixed z-50"
          variants={headerVariants}
          initial="visible"
          animate={controls}
          transition={{ duration: 0.3, ease: "linear" }}
          style={{ background: headerBackground }}
        >
          {topbar && (
            <motion.div
              className={cn(
                headerContainerStyle,
                "bg-black/90 overflow-hidden duration-200"
              )}
              style={{
                height: topBarShow,
                padding: topBarPadding,
                opacity: topBarOpacity,
              }}
            >
              <TopBar />
            </motion.div>
          )}
          <div
            className={cn(
              headerContainerStyle,
              "border-b-1 border-b-mau-do-color"
            )}
          >
            <MotionNav className="flex items-center w-full justify-between layoutContainer not-lg:py-2 px-2">
              <HeaderLogo height={imageHeight} />
              <HeaderNav motionHeight={navHeight} />
              <div className="flex items-center">
                <HeaderRightActions user={user} />
                <HeaderNavMobile user={user} />
              </div>
            </MotionNav>
          </div>

          {subHeader && (
            <div className={cn(headerContainerStyle, "")}>
              <SubHeader />
            </div>
          )}
        </MotionHeader>
      )}
    </>
  );
}
