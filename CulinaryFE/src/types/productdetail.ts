export interface Review {
  reviewId: string;
  customerName: string;
  rating: number;
  comment: string;
  createdAt: string;
}

export interface ProductDetail {
  productId: string;
  productName: string;
  shortDescription: string;
  longDescription: string;
  price: number;
  discount: number;
  finalPrice: number;
  categoryName: string[];
  productImages: string[];
  reviews: Review[];
}