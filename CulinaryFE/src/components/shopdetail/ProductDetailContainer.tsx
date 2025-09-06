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
    const { slug, id } = useParams<{ slug: string; id: string }>();

    useEffect(() => {
        if (id) {
            selectProductDetail(dispatch, id)
        }
    }, [])
    return (
        <div className='py-20'>
            <ProductMain productdetail={productDetail.product} />
            <ProductDetail productdetail={productDetail.product} />
            <RelatedProduct productdetail={productDetail.product} />
        </div>
    )
}
