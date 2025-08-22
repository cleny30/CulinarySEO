import type { ReactNode } from 'react';
import React from 'react';
import Header from './header';
import Footer from './footer';

interface ProductLayoutProps {
  children: ReactNode;
}

const ProductLayout: React.FC<ProductLayoutProps> = ({ children }) => {
  return (
    <div className="flex flex-col min-h-screen">
      <Header subHeader/>
      <main className="flex-grow">
        {children}
      </main>
      <Footer />
    </div>
  );
};

export default ProductLayout;
