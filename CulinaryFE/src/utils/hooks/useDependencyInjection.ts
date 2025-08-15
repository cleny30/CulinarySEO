import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";

export const useDependencyInjection = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  return { dispatch, navigate };
};
