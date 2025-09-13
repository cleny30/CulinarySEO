import { useSelector } from "react-redux";
import { useNavigate, useSearchParams } from "react-router-dom";
import type { RootState } from "@/redux/store";

export function useCategoryNavigator() {
  const filter = useSelector((state: RootState) => state.productfilter.productfilter);
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  // Redux → URL Navigate on category change
  const handleCategoryChange = (catId: number | null) => {
    const cat = filter.categories.find(c => c.categoryId === catId);
    const slug = cat ? cat.slug : "all";

    const query = searchParams.toString();
    navigate(`/collection/${slug}${query ? "?" + query : ""}`);
  };

  return { handleCategoryChange };
}
