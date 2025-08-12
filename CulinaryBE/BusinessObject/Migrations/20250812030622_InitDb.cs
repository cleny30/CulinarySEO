using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Pgvector;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:vector", ",,");

            migrationBuilder.CreateTable(
                name: "blog_categories",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    category_image = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_categories", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    category_image = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    profile_pic = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    status = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    token = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    revoked = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    permission_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    permission_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.permission_id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "vouchers",
                columns: table => new
                {
                    voucher_id = table.Column<Guid>(type: "uuid", nullable: false),
                    voucher_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    discount_type = table.Column<string>(type: "text", nullable: false),
                    discount_value = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    min_order_value = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    max_discount_value = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    max_vouchers_per_order = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    usage_limit = table.Column<int>(type: "integer", nullable: false),
                    used_count = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vouchers", x => x.voucher_id);
                });

            migrationBuilder.CreateTable(
                name: "warehouses",
                columns: table => new
                {
                    warehouse_id = table.Column<Guid>(type: "uuid", nullable: false),
                    warehouse_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    location = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouses", x => x.warehouse_id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    discount = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.product_id);
                    table.ForeignKey(
                        name: "FK_products_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "blogs",
                columns: table => new
                {
                    blog_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blogs", x => x.blog_id);
                    table.ForeignKey(
                        name: "FK_blogs_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    cart_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts", x => x.cart_id);
                    table.ForeignKey(
                        name: "FK_carts_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customer_addresses",
                columns: table => new
                {
                    address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    is_default = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_addresses", x => x.address_id);
                    table.ForeignKey(
                        name: "FK_customer_addresses_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationCustomers",
                columns: table => new
                {
                    notification_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    message = table.Column<string>(type: "character varying(125)", maxLength: 125, nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_read = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationCustomers", x => x.notification_id);
                    table.ForeignKey(
                        name: "FK_NotificationCustomers_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "managers",
                columns: table => new
                {
                    manager_id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    token = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    revoked = table.Column<bool>(type: "boolean", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_managers", x => x.manager_id);
                    table.ForeignKey(
                        name: "FK_managers_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    permission_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permissions", x => new { x.role_id, x.permission_id });
                    table.ForeignKey(
                        name: "FK_role_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permissions",
                        principalColumn: "permission_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permissions_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_images",
                columns: table => new
                {
                    image_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    image_url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_images", x => x.image_id);
                    table.ForeignKey(
                        name: "FK_product_images_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_reviews",
                columns: table => new
                {
                    review_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: true),
                    comment = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_reviews", x => x.review_id);
                    table.ForeignKey(
                        name: "FK_product_reviews_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_product_reviews_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stock",
                columns: table => new
                {
                    stock_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    warehouse_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    last_updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stock", x => x.stock_id);
                    table.ForeignKey(
                        name: "FK_stock_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stock_warehouses_warehouse_id",
                        column: x => x.warehouse_id,
                        principalTable: "warehouses",
                        principalColumn: "warehouse_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "blog_category_mappings",
                columns: table => new
                {
                    blog_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_category_mappings", x => new { x.blog_id, x.category_id });
                    table.ForeignKey(
                        name: "FK_blog_category_mappings_blog_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "blog_categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_blog_category_mappings_blogs_blog_id",
                        column: x => x.blog_id,
                        principalTable: "blogs",
                        principalColumn: "blog_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "blog_comments",
                columns: table => new
                {
                    comment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    blog_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    parent_comment_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_comments", x => x.comment_id);
                    table.ForeignKey(
                        name: "FK_blog_comments_blog_comments_parent_comment_id",
                        column: x => x.parent_comment_id,
                        principalTable: "blog_comments",
                        principalColumn: "comment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_blog_comments_blogs_blog_id",
                        column: x => x.blog_id,
                        principalTable: "blogs",
                        principalColumn: "blog_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_blog_comments_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "blog_images",
                columns: table => new
                {
                    image_id = table.Column<Guid>(type: "uuid", nullable: false),
                    blog_id = table.Column<Guid>(type: "uuid", nullable: false),
                    image_url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_images", x => x.image_id);
                    table.ForeignKey(
                        name: "FK_blog_images_blogs_blog_id",
                        column: x => x.blog_id,
                        principalTable: "blogs",
                        principalColumn: "blog_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "blog_saves",
                columns: table => new
                {
                    save_id = table.Column<Guid>(type: "uuid", nullable: false),
                    blog_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    saved_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_saves", x => x.save_id);
                    table.ForeignKey(
                        name: "FK_blog_saves_blogs_blog_id",
                        column: x => x.blog_id,
                        principalTable: "blogs",
                        principalColumn: "blog_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_blog_saves_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cart_items",
                columns: table => new
                {
                    cart_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cart_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    added_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart_items", x => x.cart_item_id);
                    table.ForeignKey(
                        name: "FK_cart_items_carts_cart_id",
                        column: x => x.cart_id,
                        principalTable: "carts",
                        principalColumn: "cart_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cart_items_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chat_sessions",
                columns: table => new
                {
                    chat_session_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    manager_id = table.Column<Guid>(type: "uuid", nullable: false),
                    support_type = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_sessions", x => x.chat_session_id);
                    table.ForeignKey(
                        name: "FK_chat_sessions_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chat_sessions_managers_manager_id",
                        column: x => x.manager_id,
                        principalTable: "managers",
                        principalColumn: "manager_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "NotificationManagers",
                columns: table => new
                {
                    notification_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    manager_id = table.Column<Guid>(type: "uuid", nullable: false),
                    message = table.Column<string>(type: "character varying(125)", maxLength: 125, nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_read = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationManagers", x => x.notification_id);
                    table.ForeignKey(
                        name: "FK_NotificationManagers_managers_manager_id",
                        column: x => x.manager_id,
                        principalTable: "managers",
                        principalColumn: "manager_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_status = table.Column<string>(type: "text", nullable: false),
                    shipping_fee = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    shipping_address = table.Column<string>(type: "text", nullable: false),
                    shipper_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_orders_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_orders_managers_shipper_id",
                        column: x => x.shipper_id,
                        principalTable: "managers",
                        principalColumn: "manager_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "product_history",
                columns: table => new
                {
                    history_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    discount = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    changed_by = table.Column<Guid>(type: "uuid", nullable: false),
                    change_reason = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    changed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_history", x => x.history_id);
                    table.ForeignKey(
                        name: "FK_product_history_managers_changed_by",
                        column: x => x.changed_by,
                        principalTable: "managers",
                        principalColumn: "manager_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_product_history_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shipper_status",
                columns: table => new
                {
                    shipper_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    last_updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipper_status", x => x.shipper_id);
                    table.ForeignKey(
                        name: "FK_shipper_status_managers_shipper_id",
                        column: x => x.shipper_id,
                        principalTable: "managers",
                        principalColumn: "manager_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stock_transactions",
                columns: table => new
                {
                    transaction_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    warehouse_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity_change = table.Column<int>(type: "integer", nullable: false),
                    transaction_type = table.Column<string>(type: "text", nullable: false),
                    reason = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expiration_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    manager_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stock_transactions", x => x.transaction_id);
                    table.ForeignKey(
                        name: "FK_stock_transactions_managers_manager_id",
                        column: x => x.manager_id,
                        principalTable: "managers",
                        principalColumn: "manager_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_stock_transactions_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stock_transactions_warehouses_warehouse_id",
                        column: x => x.warehouse_id,
                        principalTable: "warehouses",
                        principalColumn: "warehouse_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_images_embedding",
                columns: table => new
                {
                    embedding_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    image_id = table.Column<Guid>(type: "uuid", nullable: false),
                    image_embedding_yolo = table.Column<Vector>(type: "vector(3)", nullable: false),
                    description_embed = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_images_embedding", x => x.embedding_id);
                    table.ForeignKey(
                        name: "FK_product_images_embedding_product_images_image_id",
                        column: x => x.image_id,
                        principalTable: "product_images",
                        principalColumn: "image_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_images_embedding_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chat_histories",
                columns: table => new
                {
                    chat_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    message = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_histories", x => x.chat_id);
                    table.ForeignKey(
                        name: "FK_chat_histories_chat_sessions_ChatSessionId",
                        column: x => x.ChatSessionId,
                        principalTable: "chat_sessions",
                        principalColumn: "chat_session_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "delivery_slots",
                columns: table => new
                {
                    slot_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery_slots", x => x.slot_id);
                    table.ForeignKey(
                        name: "FK_delivery_slots_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_details",
                columns: table => new
                {
                    order_detail_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    subtotal = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_details", x => x.order_detail_id);
                    table.ForeignKey(
                        name: "FK_order_details_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_details_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "order_status_history",
                columns: table => new
                {
                    history_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    changed_by = table.Column<Guid>(type: "uuid", nullable: true),
                    change_note = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    changed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_status_history", x => x.history_id);
                    table.ForeignKey(
                        name: "FK_order_status_history_managers_changed_by",
                        column: x => x.changed_by,
                        principalTable: "managers",
                        principalColumn: "manager_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_order_status_history_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_vouchers",
                columns: table => new
                {
                    order_voucher_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    voucher_id = table.Column<Guid>(type: "uuid", nullable: false),
                    applied_discount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    applied_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_vouchers", x => x.order_voucher_id);
                    table.ForeignKey(
                        name: "FK_order_vouchers_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_vouchers_vouchers_voucher_id",
                        column: x => x.voucher_id,
                        principalTable: "vouchers",
                        principalColumn: "voucher_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "idx_category_bloc_name",
                table: "blog_categories",
                column: "category_name");

            migrationBuilder.CreateIndex(
                name: "idx_blog_id_category",
                table: "blog_category_mappings",
                column: "blog_id");

            migrationBuilder.CreateIndex(
                name: "idx_category_id_blog",
                table: "blog_category_mappings",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "idx_blog_id_comment",
                table: "blog_comments",
                column: "blog_id");

            migrationBuilder.CreateIndex(
                name: "idx_customer_id_comment",
                table: "blog_comments",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "idx_parent_comment_id",
                table: "blog_comments",
                column: "parent_comment_id");

            migrationBuilder.CreateIndex(
                name: "idx_blog_id",
                table: "blog_images",
                column: "blog_id");

            migrationBuilder.CreateIndex(
                name: "idx_blog_id_save",
                table: "blog_saves",
                column: "blog_id");

            migrationBuilder.CreateIndex(
                name: "idx_customer_id_save",
                table: "blog_saves",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "idx_customer_id_blog",
                table: "blogs",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "idx_cart_id",
                table: "cart_items",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "idx_product_id_cart_item",
                table: "cart_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "idx_customer_id_cart",
                table: "carts",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "idx_category_name",
                table: "categories",
                column: "category_name");

            migrationBuilder.CreateIndex(
                name: "IX_chat_histories_ChatSessionId",
                table: "chat_histories",
                column: "ChatSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_chat_sessions_customer_id",
                table: "chat_sessions",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_sessions_manager_id",
                table: "chat_sessions",
                column: "manager_id");

            migrationBuilder.CreateIndex(
                name: "idx_customer_id_address",
                table: "customer_addresses",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "idx_is_default",
                table: "customer_addresses",
                column: "is_default");

            migrationBuilder.CreateIndex(
                name: "idx_customer_status",
                table: "customers",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "idx_email_customer",
                table: "customers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_full_name",
                table: "customers",
                column: "full_name");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_slots_order_id",
                table: "delivery_slots",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "idx_email",
                table: "managers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_role_id",
                table: "managers",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "idx_username",
                table: "managers",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationCustomers_customer_id",
                table: "NotificationCustomers",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationManagers_manager_id",
                table: "NotificationManagers",
                column: "manager_id");

            migrationBuilder.CreateIndex(
                name: "idx_order_id",
                table: "order_details",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "idx_product_id_order_detail",
                table: "order_details",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "idx_changed_by_history",
                table: "order_status_history",
                column: "changed_by");

            migrationBuilder.CreateIndex(
                name: "idx_order_id_history",
                table: "order_status_history",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "idx_status",
                table: "order_status_history",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "idx_order_id_voucher",
                table: "order_vouchers",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "idx_voucher_id_order",
                table: "order_vouchers",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "idx_customer_id_order",
                table: "orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "idx_order_status",
                table: "orders",
                column: "order_status");

            migrationBuilder.CreateIndex(
                name: "idx_shipper_id",
                table: "orders",
                column: "shipper_id");

            migrationBuilder.CreateIndex(
                name: "idx_permission_name",
                table: "permissions",
                column: "permission_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_changed_by",
                table: "product_history",
                column: "changed_by");

            migrationBuilder.CreateIndex(
                name: "idx_product_id_history",
                table: "product_history",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "idx_product_id",
                table: "product_images",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_images_embedding_image_id",
                table: "product_images_embedding",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_images_embedding_product_id",
                table: "product_images_embedding",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "idx_customer_id_review",
                table: "product_reviews",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "idx_product_id_review",
                table: "product_reviews",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "idx_category_id",
                table: "products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "idx_product_name",
                table: "products",
                column: "product_name");

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_permission_id",
                table: "role_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "idx_role_name",
                table: "roles",
                column: "role_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_ship_status",
                table: "shipper_status",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "idx_product_id_stock",
                table: "stock",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "idx_warehouse_id",
                table: "stock",
                column: "warehouse_id");

            migrationBuilder.CreateIndex(
                name: "idx_manager_id_transaction",
                table: "stock_transactions",
                column: "manager_id");

            migrationBuilder.CreateIndex(
                name: "idx_product_id_transaction",
                table: "stock_transactions",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "idx_warehouse_id_transaction",
                table: "stock_transactions",
                column: "warehouse_id");

            migrationBuilder.CreateIndex(
                name: "idx_date_range",
                table: "vouchers",
                columns: new[] { "start_date", "end_date" });

            migrationBuilder.CreateIndex(
                name: "idx_status_voucher",
                table: "vouchers",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "idx_voucher_code",
                table: "vouchers",
                column: "voucher_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_warehouse_name",
                table: "warehouses",
                column: "warehouse_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blog_category_mappings");

            migrationBuilder.DropTable(
                name: "blog_comments");

            migrationBuilder.DropTable(
                name: "blog_images");

            migrationBuilder.DropTable(
                name: "blog_saves");

            migrationBuilder.DropTable(
                name: "cart_items");

            migrationBuilder.DropTable(
                name: "chat_histories");

            migrationBuilder.DropTable(
                name: "customer_addresses");

            migrationBuilder.DropTable(
                name: "delivery_slots");

            migrationBuilder.DropTable(
                name: "NotificationCustomers");

            migrationBuilder.DropTable(
                name: "NotificationManagers");

            migrationBuilder.DropTable(
                name: "order_details");

            migrationBuilder.DropTable(
                name: "order_status_history");

            migrationBuilder.DropTable(
                name: "order_vouchers");

            migrationBuilder.DropTable(
                name: "product_history");

            migrationBuilder.DropTable(
                name: "product_images_embedding");

            migrationBuilder.DropTable(
                name: "product_reviews");

            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropTable(
                name: "shipper_status");

            migrationBuilder.DropTable(
                name: "stock");

            migrationBuilder.DropTable(
                name: "stock_transactions");

            migrationBuilder.DropTable(
                name: "blog_categories");

            migrationBuilder.DropTable(
                name: "blogs");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropTable(
                name: "chat_sessions");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "vouchers");

            migrationBuilder.DropTable(
                name: "product_images");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "warehouses");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "managers");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
