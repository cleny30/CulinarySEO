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
        const categories = searchParams.getAll("filter.v.category"); // string[]
        if (categories.length) {
            dispatch(setSelectedCategories(categories.map(Number))); // number[]
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

        if (filter.selectedCategories?.length) {
            filter.selectedCategories.forEach((catId: number) => {
                params.append("filter.v.category", String(catId));
            });
        }
        if (filter.price?.from !== undefined)
            params.set("filter.v.price.gte", String(filter.price.from));
        if (filter.price?.to !== undefined)
            params.set("filter.v.price.lte", String(filter.price.to));

        if (filter.availability !== null)
            params.set("filter.v.availability", filter.availability ? "1" : "0");

        if (filter.sortBy !== null) params.set("sortBy", String(filter.sortBy));

        const newSearch = params.toString();
        const currentSearch = location.search.replace(/^\?/, "");

        if (newSearch !== currentSearch) {
            navigate(`/shop?${newSearch}`, { replace: true });
        }
    }, [filter, navigate, location.search]);
}
