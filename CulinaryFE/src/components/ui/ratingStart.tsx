import React, { useCallback, useEffect, useId, useMemo, useRef, useState } from "react";
import { Star } from "lucide-react";
import { motion } from "framer-motion";

/**
 * RatingStar Component — accessible, keyboard-friendly star rating
 * - Uses lucide-react <Star /> icons
 * - Supports half steps, clearable selection, controlled/uncontrolled modes
 * - Works with keyboard (Arrow keys, Home/End), mouse, and touch
 * - Customizable size, color, count, gaps, readOnly/disabled, tooltips
 */

export type RatingValue = number; // supports halves, e.g., 3.5

export interface RatingStarProps {
  /** Number of stars to display */
  count?: number;
  /** Current value (controlled) */
  value?: RatingValue;
  /** Initial value (uncontrolled) */
  defaultValue?: RatingValue;
  /** Callback when value changes */
  onChange?: (value: RatingValue) => void;
  /** Allow 0.5 step selection */
  allowHalf?: boolean;
  /** When true, clicking the same value clears to 0 */
  allowClear?: boolean;
  /** Disable all interactions */
  disabled?: boolean;
  /** Read-only (visual only, focusable=false) */
  readOnly?: boolean;
  /** Size in px for the star icon */
  size?: number;
  /** Tailwind text color for filled part (e.g., 'text-yellow-500') */
  colorClass?: string;
  /** Tailwind text color for empty part (e.g., 'text-gray-300') */
  emptyColorClass?: string;
  /** Gap between stars (Tailwind spacing class) */
  gapClass?: string;
  /** aria-label for the whole control */
  ariaLabel?: string;
  /** Optional tooltip labels per star index (1..count). If using half, label may show 0.5 steps automatically */
  tooltips?: string[];
  /** Name for form integration (hidden input mirrors the value) */
  name?: string;
  /** Minimum step (0.5 or 1) computed from allowHalf */
  step?: 0.5 | 1;
}

function clamp(v: number, min: number, max: number) {
  return Math.max(min, Math.min(max, v));
}

function roundToStep(v: number, step: number) {
  return Math.round(v / step) * step;
}

export function RatingStar({
  count = 5,
  value,
  defaultValue = 0,
  onChange,
  allowHalf = true,
  allowClear = true,
  disabled = false,
  readOnly = false,
  size = 28,
  colorClass = "text-yellow-500",
  emptyColorClass = "text-gray-300",
  gapClass = "gap-1.5",
  ariaLabel = "Rating",
  tooltips,
  name,
  step,
}: RatingStarProps) {
  const actualStep = step ?? (allowHalf ? 0.5 : 1);
  const isControlled = value !== undefined;
  const [inner, setInner] = useState<RatingValue>(clamp(defaultValue, 0, count));
  const current = clamp(isControlled ? (value as number) : inner, 0, count);
  const [hover, setHover] = useState<RatingValue | null>(null);
  const id = useId();
  const rootRef = useRef<HTMLDivElement | null>(null);

  useEffect(() => {
    if (isControlled) return;
    setInner(clamp(defaultValue, 0, count));
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [defaultValue, count]);

  const displayValue = hover ?? current;

  const commitChange = useCallback(
    (next: RatingValue) => {
      const nextClamped = clamp(roundToStep(next, actualStep), 0, count);
      const same = nextClamped === current;
      const final = allowClear && same ? 0 : nextClamped;

      if (!isControlled) setInner(final);
      onChange?.(final);
    },
    [actualStep, allowClear, count, current, isControlled, onChange]
  );

  const handleKeyDown = useCallback(
    (e: React.KeyboardEvent) => {
      if (disabled || readOnly) return;
      let next = current;
      if (e.key === "ArrowRight" || e.key === "ArrowUp") {
        next = clamp(current + actualStep, 0, count);
        e.preventDefault();
        commitChange(next);
      } else if (e.key === "ArrowLeft" || e.key === "ArrowDown") {
        next = clamp(current - actualStep, 0, count);
        e.preventDefault();
        commitChange(next);
      } else if (e.key === "Home") {
        e.preventDefault();
        commitChange(0);
      } else if (e.key === "End") {
        e.preventDefault();
        commitChange(count);
      } else if (e.key === " ") {
        // Space toggles clear when allowed
        e.preventDefault();
        if (allowClear) commitChange(0);
      }
    },
    [actualStep, commitChange, count, current, disabled, readOnly]
  );

  const starNodes = useMemo(() => {
    const nodes: React.ReactNode[] = [];

    for (let i = 1; i <= count; i++) {
      const filledPortion = clamp(displayValue - (i - 1), 0, 1); // 0..1
      const fillPercent = Math.round(filledPortion * 100);
      const labelBase = tooltips?.[i - 1] ?? `${i} star${i > 1 ? "s" : ""}`;
      const label = allowHalf && fillPercent === 50 ? `${i - 0.5} — ${labelBase}` : labelBase;

      const interactiveProps = disabled || readOnly
        ? {}
        : {
            onMouseMove: (e: React.MouseEvent) => {
              if (!allowHalf) return;
              const target = e.currentTarget as HTMLDivElement;
              const rect = target.getBoundingClientRect();
              const x = e.clientX - rect.left;
              const half = x < rect.width / 2 ? 0.5 : 1;
              setHover(i - 1 + half);
            },
            onMouseLeave: () => setHover(null),
            onClick: (e: React.MouseEvent) => {
              if (allowHalf) {
                const target = e.currentTarget as HTMLDivElement;
                const rect = target.getBoundingClientRect();
                const x = e.clientX - rect.left;
                const half = x < rect.width / 2 ? 0.5 : 1;
                commitChange(i - 1 + half);
              } else {
                commitChange(i);
              }
            },
            onTouchStart: (e: React.TouchEvent) => {
              if (!allowHalf) return;
              const touch = e.touches[0];
              const target = e.currentTarget as HTMLDivElement;
              const rect = target.getBoundingClientRect();
              const x = touch.clientX - rect.left;
              const half = x < rect.width / 2 ? 0.5 : 1;
              setHover(i - 1 + half);
            },
            onTouchEnd: () => setHover(null),
          };

      nodes.push(
        <div
          key={i}
          role="radio"
          aria-checked={displayValue >= i - (allowHalf ? 0.5 : 0)}
          aria-label={label}
          className={`relative inline-flex ${disabled ? "cursor-not-allowed" : readOnly ? "cursor-default" : "cursor-pointer"}`}
          style={{ width: size, height: size }}
          {...interactiveProps}
        >
          {/* Base (empty) star */}
          <Star
            aria-hidden
            className={`${emptyColorClass}`}
            style={{ width: size, height: size }}
          />

          {/* Filled overlay with clip (handles halves & partials) */}
          <div
            className="absolute inset-0 overflow-hidden pointer-events-none"
            style={{ width: `${fillPercent}%` }}
          >
            <Star
              aria-hidden
              className={`${colorClass}`}
              style={{ width: size, height: size }}
              fill="currentColor"
            />
          </div>
        </div>
      );
    }

    return nodes;
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [allowHalf, commitChange, count, disabled, displayValue, emptyColorClass, readOnly, size, colorClass, tooltips]);

  return (
    <div className="inline-flex flex-col" aria-disabled={disabled} aria-readonly={readOnly}>
      <div
        id={id}
        ref={rootRef}
        role="radiogroup"
        aria-label={ariaLabel}
        tabIndex={readOnly || disabled ? -1 : 0}
        onKeyDown={handleKeyDown}
        className={`inline-flex items-center ${gapClass} outline-none focus-visible:ring-2 focus-visible:ring-offset-2 focus-visible:ring-yellow-500 rounded-xl`}
      >
        {starNodes}
      </div>
      {name && (
        <input type="hidden" name={name} value={String(current)} />
      )}
    </div>
  );
}

/**
 * Demo component showing common configurations
 */
export default function RatingStarDemo() {
  const [rating, setRating] = useState<RatingValue>(3.5);

  return (
    <div className="w-full min-h-[60vh] grid place-items-center p-6">
      <div className="w-full max-w-2xl space-y-8">
        <header className="space-y-1">
          <h1 className="text-2xl font-semibold">Lucide RatingStar</h1>
          <p className="text-sm text-gray-500">Accessible star rating with half-steps, keyboard support, and full customization.</p>
        </header>

        {/* Controlled */}
        <div className="p-5 rounded-2xl shadow-sm border bg-white">
          <div className="flex items-center justify-between">
            <div className="space-y-2">
              <h2 className="font-medium">Controlled</h2>
              <p className="text-sm text-gray-500">Value: {rating}</p>
              <div className="flex items-center gap-3">
                <RatingStar
                  value={rating}
                  onChange={setRating}
                  allowHalf
                  allowClear
                  colorClass="text-yellow-500"
                  emptyColorClass="text-gray-300"
                  size={32}
                  tooltips={["Terrible", "Bad", "Okay", "Good", "Excellent"]}
                  ariaLabel="Product rating"
                  name="rating"
                />
                <motion.button
                  whileTap={{ scale: 0.97 }}
                  className="px-3 py-1.5 text-sm rounded-xl border bg-white"
                  onClick={() => setRating(0)}
                >
                  Reset
                </motion.button>
              </div>
            </div>
          </div>
        </div>

        {/* Uncontrolled */}
        <div className="p-5 rounded-2xl shadow-sm border bg-white">
          <h2 className="font-medium mb-2">Uncontrolled</h2>
          <RatingStar defaultValue={2} />
        </div>

        {/* Sizes & states */}
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
          <div className="p-5 rounded-2xl shadow-sm border bg-white">
            <h3 className="font-medium mb-2">Compact</h3>
            <RatingStar defaultValue={4.5} size={20} gapClass="gap-1" />
          </div>
          <div className="p-5 rounded-2xl shadow-sm border bg-white">
            <h3 className="font-medium mb-2">Disabled</h3>
            <RatingStar value={3} disabled />
          </div>
          <div className="p-5 rounded-2xl shadow-sm border bg-white">
            <h3 className="font-medium mb-2">Read-only</h3>
            <RatingStar value={4} readOnly />
          </div>
          <div className="p-5 rounded-2xl shadow-sm border bg-white">
            <h3 className="font-medium mb-2">No halves</h3>
            <RatingStar defaultValue={3} allowHalf={false} />
          </div>
        </div>

        {/* Form example */}
        <form
          onSubmit={(e) => {
            e.preventDefault();
            const data = new FormData(e.currentTarget);
            alert(`Submitted rating: ${data.get("formRating")} / 5`);
          }}
          className="p-5 rounded-2xl shadow-sm border bg-white space-y-3"
        >
          <h3 className="font-medium">Form integration</h3>
          <RatingStar name="formRating" defaultValue={4} />
          <button type="submit" className="px-3 py-1.5 text-sm rounded-xl border bg-white">Submit</button>
        </form>

        <footer className="text-xs text-gray-500">
          Tip: Use Arrow keys to change, Home/End for min/max, and Space to clear.
        </footer>
      </div>
    </div>
  );
}
