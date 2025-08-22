import type { UserSession } from "@/types/userSession";
import { Icon } from "@/utils/assets/icon";
import React from "react";
import { Link } from "react-router-dom";

export default function HeaderCart({ user }: { user: UserSession | null }) {
  return (
    <>
      <Link
        to={user ? "" : "/login"}
        className="rounded-4xl p-2 hover:bg-white/20 hover:shadow-sm ease-in-out duration-300"
      >
        <Icon.ShoppingCart className="h-6 w-6" />
      </Link>
    </>
  );
}
