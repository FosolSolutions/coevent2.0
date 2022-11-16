import { SelectHTMLAttributes } from 'react';

import { DropdownVariant, instanceOfIOption, IOption } from '.';
import * as styled from './DropdownStyled';

export interface IDropdownProps<T> extends Omit<SelectHTMLAttributes<HTMLSelectElement>, 'value'> {
  /**
   * The styled variant.
   */
  variant?: DropdownVariant;
  /**
   * The tooltip to show on hover.
   */
  tooltip?: string;
  /**
   * An array of options.
   */
  options?: readonly string[] | number[] | IOption[] | HTMLOptionElement[] | T[];
  /**
   * Property name of option item that is used to match the selected value(s).
   */
  optionValue?: string;
  /**
   * Property name of the option item that is used to match the text.
   */
  optionText?: string;
  /**
   * The selected values.
   */
  value?: string | number | T | readonly string[] | readonly T[];
}

/**
 * Dropdown component provides a bootstrapped styled button element.
 * @param param0 Dropdown element attributes.
 * @returns Dropdown component.
 */
export const Dropdown = <T,>({
  variant = DropdownVariant.primary,
  tooltip,
  children,
  className,
  options,
  optionValue,
  optionText,
  value,
  ...rest
}: IDropdownProps<T>) => {
  const getValue = () => {
    if (value === undefined || !Array.isArray(value) || !value.length) return value;
    if (value[0] instanceof HTMLOptionElement) return value.map((o) => o.value);
    if (typeof value[0] === 'string' || typeof value[0] === 'number') return value;
    if (optionValue) return value.map((v) => (v as any)[optionValue]);
    return value;
  };

  return (
    <styled.Dropdown
      variant={variant}
      value={getValue()}
      className={`${className}`}
      data-for="main"
      data-tip={tooltip}
      {...rest}
    >
      {options
        ? options.map((option) => {
            if (instanceOfIOption(option)) {
              const item = option as IOption;
              return (
                <option key={item.value} value={item.value}>
                  {item.label}
                </option>
              );
            } else if (option instanceof HTMLOptionElement) {
              const element = option as HTMLOptionElement;
              return (
                <option key={element.id ? element.id : element.value} value={element.value}>
                  {element.textContent}
                </option>
              );
            } else if (typeof option === 'string' || typeof option === 'number') {
              return (
                <option key={option} value={option}>
                  {option}
                </option>
              );
            } else if (optionValue && typeof option === 'object') {
              const value = option as any;
              return (
                <option key={value[optionValue]} value={value[optionValue]}>
                  {value[optionText ?? optionValue]}
                </option>
              );
            }
            return <></>;
          })
        : children}
    </styled.Dropdown>
  );
};
