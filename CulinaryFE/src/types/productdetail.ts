export interface Review {
  reviewId: string;
  customerName: string;
  rating: number;
  comment: string;
  createdAt: string;
}

export interface ProductDetail {
  productId: string | null;
  productName: string | null;
  shortDescription: string | null;
  longDescription: string | null;
  price: number | null;
  discount: number | null;
  finalPrice: number | null;
  averageRating:number | null;
  slug:string | null;
  pageTitle: string | null;
  metaDescription: string | null;
  categoryId: number[] | null;
  productImages: string[] | null;
  reviews: Review[] | null;
}