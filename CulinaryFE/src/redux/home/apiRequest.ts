import { getCateList } from "@/services/homeService";
import type { AppDispatch } from "../store";
import {
  loadedCate,
  loadedHomeCate,
  loadingCate,
  loadingHomeCate,
} from "./homeSlice";
import { menuNav } from "@/utils/config/navMenu";
import type { Category } from "@/types/category";

export const fetchCateMenu = async (dispatch: AppDispatch) => {
  dispatch(loadingCate());
  dispatch(loadingHomeCate());
  const response = (await getCateList()) as Category[];

  const newCategories = response.map((item) => {
    return {
      label: item.categoryName,
      href: `/collections/${item.categoryId}`,
      image: item.categoryImage,
    };
  });

  const updatedMenuNav = menuNav.map((item) => {
    if (item.slug === "thuc-don") {
      return {
        ...item,
        children: newCategories,
      };
    }
    return item;
  });
  dispatch(loadedCate(updatedMenuNav));
  dispatch(loadedHomeCate(response));
};
