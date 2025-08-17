import type { ProductResult, Product } from "@/types/product";

function getProductPrice(product: Product): number {
  return product.finalPrice ?? product.price;
}

function formatCurrency(value: number): string {
  return new Intl.NumberFormat("vi-VN", {
    style: "currency",
    currency: "VND",
    maximumFractionDigits: 0,
  }).format(value);
}

export function getMaxPrice(products: ProductResult | null): {
  raw: number;
  formatted: string;
} {
  if (!products || !products.items || products.items.length === 0) {
    return { raw: 0, formatted: formatCurrency(0) };
  }

  const max = Math.max(...products.items.map(getProductPrice));
  return { raw: max, formatted: formatCurrency(max) };
}
