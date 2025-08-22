import React from 'react';

const Footer: React.FC = () => {
  return (
    <footer className="bg-gray-800 text-white py-8">
      <div className="container mx-auto px-4">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
          <div>
            <h3 className="text-lg font-bold mb-4">CulinarySEO</h3>
            <p>Your one-stop shop for the finest culinary products.</p>
          </div>
          <div>
            <h3 className="text-lg font-bold mb-4">Quick Links</h3>
            <ul>
              <li><a href="/" className="hover:underline">Home</a></li>
              <li><a href="/shop" className="hover:underline">Shop</a></li>
              <li><a href="/about" className="hover:underline">About Us</a></li>
              <li><a href="/contact" className="hover:underline">Contact</a></li>
            </ul>
          </div>
          <div>
            <h3 className="text-lg font-bold mb-4">Follow Us</h3>
            <div className="flex space-x-4">
              <a href="#" className="hover:text-gray-400">Facebook</a>
              <a href="#" className="hover:text-gray-400">Instagram</a>
              <a href="#" className="hover:text-gray-400">Twitter</a>
            </div>
          </div>
        </div>
        <div className="text-center mt-8 pt-4 border-t border-gray-700">
          <p>&copy; {new Date().getFullYear()} CulinarySEO. All Rights Reserved.</p>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
