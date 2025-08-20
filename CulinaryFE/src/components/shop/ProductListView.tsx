import type { RootState } from '@/redux/store';
import type { ProductResult } from '@/types/product'
import React from 'react'
import { useSelector } from 'react-redux';

export default function ProductListView({ products }: { products: ProductResult | null }) {
    const fetchingproducts = useSelector((state: RootState) => state.productview.fetching);
  return (
    <div>ProductListView</div>
  )
}
