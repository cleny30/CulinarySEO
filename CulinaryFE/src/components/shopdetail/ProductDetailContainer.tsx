import React, { useEffect } from 'react'
import ProductMain from './ProductMain'
import ProductDetail from './ProductDetail'
import RelatedProduct from './RelatedProduct'
import { useDispatch, useSelector } from 'react-redux'
import type { RootState } from '@/redux/store'
import { useParams } from 'react-router-dom'
import { selectProductDetail } from '@/redux/productdetail/apiService'

export default function ProductDetailContainer() {
    const productDetail = useSelector((state: RootState) => (state.productdetail))
    const dispatch = useDispatch()
    useEffect(() => {
        const { productId } = useParams<{ productId: string }>()
        if (productId) {
            selectProductDetail(dispatch, productId)
        }
    }, [])
    return (
        <>
            <ProductMain productdetail={productDetail.product} />
            <ProductDetail />
            <RelatedProduct />
        </>
    )
}
