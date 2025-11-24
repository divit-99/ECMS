import { Component, type ReactNode } from "react";
import ErrorPage from "../../pages/Error/ErrorPage";

interface Props {
  children: ReactNode;
}

interface State {
  hasError: boolean;
}

export default class ErrorBoundary extends Component<Props, State> {
  constructor(props: Props) {
    super(props);
    this.state = { hasError: false };
  }

  static getDerivedStateFromError() {
    // Update state so fallback UI renders
    return { hasError: true };
  }

  componentDidCatch(error: any, errorInfo: any) {
    console.error("Unexpected UI Error:", error, errorInfo);
  }

  render() {
    if (this.state.hasError) {
      return <ErrorPage />;
    }

    return this.props.children;
  }
}