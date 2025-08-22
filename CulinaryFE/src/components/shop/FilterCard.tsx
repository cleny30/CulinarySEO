import React, { useMemo } from 'react'
import { Badge } from '../ui/badge';
import { X } from 'lucide-react';
import type { Category } from '@/types/filter';

import type { FilterFormValues } from '@/schemas/filter';
import { useSelector } from 'react-redux';
import type { RootState } from '@/redux/store';
import { getMaxPrice } from '@/utils/constants/product/product';

interface FilterCardProps {
  name: FilterFormValues;
  categories?: Category[];
}
export default function FilterCard({ name, categories }: FilterCardProps) {
  const products = useSelector((state: RootState) => state.productview)
  // const maxPrice = useMemo(() => getMaxPrice(products.products ?? null), [products.products]);
  const getFilteredCategories = () => {
    if (!categories || !name.categories) return [];
    return categories.filter(cat => name.categories.includes(cat.categoryId));
  }
  const filteredCategories = getFilteredCategories();

  return (
    <div className="flex gap-2 flex-wrap">
      {/* Category badges */}
      {name.categories && filteredCategories.map(cat => (
        <Badge key={cat.categoryId} variant="outline" className='bg-[#222222] text-white cursor-pointer flex items-center gap-1'>
          {cat.categoryName}
          <X className='cursor-pointer' />
        </Badge>
      ))}
      {/* Price badge */}
      {name.price && (name.price.from !== 0 || name.price.to !== 500000) && (
        <Badge variant="outline" className='bg-[#222222] text-white cursor-pointer flex items-center gap-1'>
          Price: {name.price.from} - {name.price.to}
          <X className='cursor-pointer' />
        </Badge>
      )}
      {/* Availability badge */}
      {name.availability && (
        <Badge variant="outline" className='bg-[#222222] text-white cursor-pointer flex items-center gap-1'>
          In Stock
          <X className='cursor-pointer' />
        </Badge>
      )}
    </div>
  );
}