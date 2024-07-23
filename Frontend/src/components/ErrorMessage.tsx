import type React from "react";
import type { MouseEventHandler } from "react";

interface ErrorMessageProps {
  message: string;
  onClear: MouseEventHandler;
}

export const ErrorMessage: React.FC<ErrorMessageProps> = ({
  message,
  onClear,
}) => {
  return (
    <div className="border-red-60 flex items-center justify-between rounded-md border bg-red-700/80 px-4 py-2 text-sm text-red-50">
      <span>{message}</span>
      <button type="button" onClick={onClear}>
        ×
      </button>
    </div>
  );
};

export default ErrorMessage;
