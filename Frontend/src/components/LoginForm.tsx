import type React from "react";
import { useState, type ChangeEvent, type FormEvent } from "react";
import { getRoute } from "../utils";

interface LoginFormProps {
  accountType: "user" | "admin";
}

const LoginForm: React.FC<LoginFormProps> = ({ accountType }) => {
  const [username, setUsername] = useState<string>("");

  function handleInput(e: ChangeEvent) {
    setUsername((e.target as HTMLInputElement).value);
  }

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();

    if (!username) {
      return;
    }

    try {
      const response = await fetch(
        `https://localhost:7160/api/login/${accountType}`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(username),
          credentials: "include",
        },
      );

      if (response.ok) {
        location.href = getRoute(accountType);
      }
    } catch (e) {
      console.error(e);
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      <input
        className="text-slate-900"
        type="text"
        placeholder="e.g. john"
        value={username}
        onChange={handleInput}
      />
      <button>Login</button>
    </form>
  );
};

export default LoginForm;
