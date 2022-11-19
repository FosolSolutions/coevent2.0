import { getIn } from 'formik';
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
  optionValue?: string | ((item: T) => string);
  /**
   * Property name of the option item that is used to match the text.
   */
  optionText?: string | ((item: T) => string);
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
    if (typeof optionValue === 'function') return value.map((v) => optionValue(v));
    if (!!optionValue) return value.map((v) => getIn(v, optionValue));
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
              const ovalue =
                typeof optionValue === 'function' ? optionValue(value) : getIn(value, optionValue);
              const text = !!optionText
                ? typeof optionText === 'function'
                  ? optionText(value)
                  : getIn(value, optionText)
                : ovalue;
              return (
                <option key={ovalue} value={ovalue}>
                  {text}
                </option>
              );
            }
            return <></>;
          })
        : children}
    </styled.Dropdown>
  );
};
