import type { ProductResult, Product } from "@/types/product";

export function formatCurrency(value: number): string {
  return new Intl.NumberFormat("vi-VN", {
    style: "currency",
    currency: "VND",
    maximumFractionDigits: 0,
  }).format(value);
}

// Now returns formatted currency string instead of number
export function getProductPrice(product: Product): string {
  const price = product.finalPrice ?? product.price;
  return formatCurrency(price);
}

export function getMaxPrice(products: ProductResult | null): {
  raw: number;
  formatted: string;
} {
  if (!products || !products.items || products.items.length === 0) {
    return { raw: 0, formatted: formatCurrency(0) };
  }

  const max = Math.max(
    ...products.items.map((p) => p.finalPrice ?? p.price)
  );

  return { raw: max, formatted: formatCurrency(max) };
}
