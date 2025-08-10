import { LoginSchema, type LoginSchemaType } from "@/schemas/auth";
import { loginUser } from "@/services/authService";

export const login = async (values: LoginSchemaType) => {
    const validatedFields = LoginSchema.safeParse(values);
    if (!validatedFields.success) {
        return { error: "Dữ liệu không hợp lệ!" };
    }

    const { email, password } = validatedFields.data;

    // Gọi hàm loginUser từ authService để thực hiện đăng nhập
    const result = await loginUser({ email, password });

    // Xử lý kết quả trả về từ service
    if (result.error) {
        return { error: result.error };
    }

    // TODO: Xử lý sau khi đăng nhập thành công
    // Ví dụ: lưu token vào localStorage, cập nhật trạng thái user trong Redux
    console.log(result.data);

    return { success: "Đăng nhập thành công!" };
};
