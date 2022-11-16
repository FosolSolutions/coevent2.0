import { useFormikContext } from 'formik';

import { Dropdown, IDropdownProps } from '..';
import * as styled from './FormikDropdownStyled';

export interface IFormikDropdownProps<T> extends IDropdownProps<T> {
  name: string;
  label?: string;
}

export const FormikDropdown = <T,>({
  id,
  name,
  label,
  value,
  multiple,
  children,
  className,
  disabled,
  onChange,
  onBlur,
  ...rest
}: IFormikDropdownProps<T>) => {
  const { values, errors, touched, handleBlur, handleChange, isSubmitting } = useFormikContext<T>();
  const error = (errors as any)[name] && (touched as any)[name] && (errors as any)[name];
  return (
    <styled.FormikDropdown>
      {label && <label htmlFor={`dpn-${name}`}>{label}</label>}
      <div>
        <Dropdown<T>
          id={id ?? `dpn-${name}`}
          name={name}
          multiple={multiple}
          value={value ?? (values as any)[name] ?? (multiple ? [] : '')}
          onChange={onChange ?? handleChange}
          onBlur={onBlur ?? handleBlur}
          className={error ? `${className} error` : className}
          disabled={disabled || isSubmitting}
          {...rest}
        >
          {children}
        </Dropdown>
        {error ? <p role="alert">{error}</p> : null}
      </div>
    </styled.FormikDropdown>
  );
};
