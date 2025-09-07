import { useEffect } from "react";
import { useDispatch, useSelector, shallowEqual } from "react-redux";
import { useNavigate, useLocation, useSearchParams } from "react-router-dom";
import type { RootState } from "@/redux/store";
import {
    setPrice,
    setAvailability,
    setSelectedCategories,
    setSortBy,
} from "@/redux/product/productfilterSlice";

export function useSyncFilterWithUrl() {
    const dispatch = useDispatch();
    const filter = useSelector(
        (state: RootState) => state.productfilter.productfilter,
        shallowEqual
    );
    const navigate = useNavigate();
    const location = useLocation();
    const [searchParams] = useSearchParams();

    // URL → Redux
    useEffect(() => {
        // 1. Path-based category (shop/:slug)
        const pathParts = location.pathname.split("/");
        const slugFromPath = pathParts[2]; // "thit-heo" or "all"

        if (slugFromPath && slugFromPath !== "all") {
            const cat = filter.categories.find(c => c.slug === slugFromPath);
            if (cat) {
                dispatch(setSelectedCategories([cat.categoryId]));
            }
        } else {
            dispatch(setSelectedCategories([])); // "all" → no filter
        }

        // 2. Query-based categories (multi-select case)
        const categoriesFromUrl = searchParams.getAll("filter.v.category");
        if (categoriesFromUrl.length) {
            const ids = filter.categories
                .filter(c => categoriesFromUrl.includes(c.slug))
                .map(c => c.categoryId);
            dispatch(setSelectedCategories(ids));
        }

        const priceGte = searchParams.get("filter.v.price.gte");
        const priceLte = searchParams.get("filter.v.price.lte");
        if (priceGte || priceLte) {
            dispatch(
                setPrice({
                    from: priceGte ? Number(priceGte) : 0,
                    to: priceLte ? Number(priceLte) : 500000,
                })
            );
        }

        const availability = searchParams.get("filter.v.availability");
        if (availability !== null) {
            dispatch(setAvailability(availability === "1"));
        }

        const sortBy = searchParams.get("sortBy");
        if (sortBy !== null) {
            dispatch(setSortBy(sortBy));
        }
    }, [dispatch, searchParams]);

    // Redux → URL
    useEffect(() => {
        const params = new URLSearchParams();

        if (filter.price?.from !== undefined)
            params.set("filter.v.price.gte", String(filter.price.from));
        if (filter.price?.to !== undefined)
            params.set("filter.v.price.lte", String(filter.price.to));

        if (filter.availability !== null)
            params.set("filter.v.availability", filter.availability ? "1" : "0");

        if (filter.sortBy !== null) params.set("sortBy", String(filter.sortBy));

        const newSearch = params.toString();
        const searchString = newSearch ? `?${newSearch}` : "";

        // 🔹 Handle category slug in path
        if (filter.selectedCategories?.length === 1) {
            const catId = filter.selectedCategories[0];
            const cat = filter.categories.find(c => c.categoryId === catId);
            if (cat) {
                navigate(`/shop/${cat.slug}${searchString}`, { replace: true });
            }
        } else {
            navigate(`/shop/all${searchString}`, { replace: true });
        }
    }, [filter, navigate]);
}
